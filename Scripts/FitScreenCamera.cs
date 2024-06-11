using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitScreenCamera : MonoBehaviour
{
    public Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, 0);
        Vector3 bottomLeft = mainCam.ViewportToWorldPoint(Vector3.zero);
        Vector3 topRight = mainCam.ViewportToWorldPoint(new Vector3(mainCam.rect.width, mainCam.rect.height));
        Vector3 screenSize = topRight - bottomLeft;

        float screenRatio = screenSize.x / screenSize.y;
        float desiredRatio = transform.localScale.x/ transform.localScale.y;

        if (screenRatio > desiredRatio )
        {
            float height = screenSize.y;
            transform.localScale = new Vector3 (height * desiredRatio, height);
        }
        else
        {
            float width = screenSize.x;
            transform.localScale = new Vector3(width, width / desiredRatio);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
