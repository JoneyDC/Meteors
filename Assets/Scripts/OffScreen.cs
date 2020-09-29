using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreen : MonoBehaviour
{
    float leftConstraint;
    float rightConstraint;
    float topConstraint;
    float bottomConstraint;
    public float buffer = 1f;

    void Start()
    {
        leftConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        rightConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        topConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        bottomConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).y;
    }


    void Update()
    {
        if(transform.position.x > rightConstraint + buffer)
        {
            transform.position = new Vector3(leftConstraint - buffer, transform.position.y);
        }
        if (transform.position.x < leftConstraint - buffer)
        {
            transform.position = new Vector3(rightConstraint + buffer, transform.position.y);
        }

        if (transform.position.y > topConstraint + buffer)
        {
            transform.position = new Vector3(transform.position.x, bottomConstraint - buffer);
        }
        if (transform.position.y < bottomConstraint - buffer)
        {
            transform.position = new Vector3(transform.position.x, topConstraint + buffer);
        }

    }
}
