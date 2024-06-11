using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Touch playerTouch;
    public Transform FitScreen, LWall, RWall;
    public GameObject[] ballPrefabs;
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = gameObject.GetComponentInParent<GameManager>();
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 0);
        StartCoroutine(manager.CreateBall(ballPrefabs[0], spawnPos, false, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            playerTouch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(playerTouch.position);

            if (touchPos.x < LWall.position.x) { touchPos.x = LWall.position.x; }
            if (touchPos.x > RWall.position.x) { touchPos.x = RWall.position.x; }


            // ended
            if (playerTouch.phase == TouchPhase.Ended)
            {
                // drop circle
                Vector3 spawnPos = new Vector3(touchPos.x, transform.position.y, 0);
                manager.DropCurrentBall();

                int index = GenerateIndex();
                StartCoroutine(manager.CreateBall(ballPrefabs[index], spawnPos, false, 0.5f));
            }

            else
            {
                float newX = touchPos.x;
                //float radius = manager.currBall.GetComponent<CircleCollider2D>().radius;
                //Debug.Log(manager.currBall.name + ": " + radius);
                //if (touchPos.x - radius < -0.5f) { newX = -0.5f + radius; }
                //else if (touchPos.x + radius > 0.5f) { newX = 0.5f - radius; }

                transform.position = new Vector3(newX, transform.position.y, 0);

                manager.MoveCurrentBall(transform.position);
            }

        }
    }

    int GenerateIndex()
    {
        float golf, tennis, baseball;
        float probability = UnityEngine.Random.Range(0.0f, 100.0f);
        if (manager.score < 300)
        {
            golf = 60.0f;
            tennis = 95.0f;
            baseball = 100.0f;
        }
        else 
        {
            golf = 25.0f;
            tennis = 60.0f;
            baseball = 85.0f;
        }

        if (0.0f <= probability && probability <= golf) { return 0; }
        else if (golf < probability && probability <= tennis) { return 1; }
        else if (tennis < probability && probability <= baseball) { return 2; }
        else { return 3; }
    }

}
