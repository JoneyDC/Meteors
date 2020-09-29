using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public float Speed;

    float leftConstraint;
    float rightConstraint;
    float topConstraint;
    float bottomConstraint;
    GameObject ScoreBoard;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        rightConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        topConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        bottomConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).y;
        ScoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard");
    }

    void FixedUpdate()
    {
        //moves the projectile in a straight line from its firing point
        rb.velocity = transform.right * Time.deltaTime * Speed;
    }
    private void Update()
    {
        if (transform.position.x > rightConstraint || transform.position.x < leftConstraint || transform.position.y > topConstraint || transform.position.y < bottomConstraint)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Meteor"))
        {
            ScoreBoard.GetComponent<ScoreSystem>().Score += collision.gameObject.GetComponent<Meteor>().Points;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
