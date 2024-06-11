using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public GameObject currBall;
    [HideInInspector] public int score;
    public TMP_Text scoreText;
    public GameObject line;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLine();
        scoreText.text = score.ToString();
    }

    void CheckLine()
    {
        float closest = float.MaxValue;
        foreach (Transform child in transform)
        {
            // if (child.gameObject.CompareTag("Ball")) { Debug.Log(child.gameObject.name + ": " + child.gameObject.GetComponent<BallManager>().isGrounded); }
            if (child.gameObject.CompareTag("Ball") && child.gameObject.GetComponent<BallManager>().isGrounded)
            {
                float radius = child.gameObject.GetComponent<CircleCollider2D>().radius;
                float distance = line.transform.position.y - (radius + child.position.y);
                //Debug.Log(child.gameObject.name + ": " + distance);
                if (distance < closest) { closest = distance; }
            }
        }

        if (closest <= 0) { line.GetComponent<SpriteRenderer>().color = Color.red; }
        else if (closest <= 0.7) { line.GetComponent<SpriteRenderer>().color = Color.yellow; }
        else { line.GetComponent<SpriteRenderer>().color = Color.white; }
    }


    public IEnumerator CreateBall(GameObject ball, Vector3 pos, bool isSimulated, float delay)
    {
        if (!isSimulated) { currBall = null; }
        yield return new WaitForSecondsRealtime(delay);

        if (currBall == null)
        {
            GameObject newBall = Instantiate(ball, pos, Quaternion.identity, transform);
            newBall.GetComponent<Rigidbody2D>().simulated = isSimulated;
            newBall.GetComponent<BallManager>().GM = this;
            if (!isSimulated) { currBall = newBall; }
        }
    }

    public void MergeBall(GameObject ball, Vector3 pos, bool isSimulated)
    {

        GameObject newBall = Instantiate(ball, pos, Quaternion.identity, transform);
        newBall.GetComponent<Rigidbody2D>().simulated = isSimulated;
        newBall.GetComponent<BallManager>().GM = this;
    }

    public void DropCurrentBall()
    {
        if (currBall != null) { currBall.GetComponent<Rigidbody2D>().simulated = true; }
    }

    public void MoveCurrentBall(Vector3 position) 
    {
        if (currBall != null) { currBall.GetComponent<Transform>().position = position; }
    }

    public void IncrementScore(int add)
    {
        score += add;
    }
}
