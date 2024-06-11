using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject parentBall;
    public int score;
    [HideInInspector] public GameManager GM;
    [HideInInspector] public bool isCollided, isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        isCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        //Debug.Log("Collision Detected");
        if (collision.gameObject.name == this.name)
        {
            //Debug.Log("Object1 name: " + this.name);
            //Debug.Log("Object2 name: " + collision.gameObject.name);

            if (!collision.gameObject.GetComponent<BallManager>().isCollided && !isCollided)
            { 

                collision.gameObject.GetComponent<BallManager>().isCollided = true;
                isCollided = true;

                Vector3 ballPos = collision.gameObject.transform.position;
                Vector3 spawnPos = new Vector3((ballPos.x + transform.position.x) / 2, (ballPos.y + transform.position.y) / 2, 1);

                GM.MergeBall(parentBall, spawnPos, true);
                GM.IncrementScore(score);
            }
                
            if (this != null) { Destroy(this); }
            if (collision.gameObject != null) { Destroy(collision.gameObject); }
            if (collision.gameObject != null) { Destroy(collision.gameObject); }
        }
    }
}
