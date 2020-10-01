using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public float Speed, Duration;
    public List<AudioClip> clip;

    float Timer;
    GameObject ScoreBoard;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ScoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard");
    }

    void FixedUpdate()
    {
        //moves the projectile in a straight line from its firing point
        rb.velocity = transform.right * Time.deltaTime * Speed;
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        if(Timer> Duration)
        {
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Meteor"))
        {
            ScoreBoard.GetComponent<ScoreSystem>().Score += collision.gameObject.GetComponent<Meteor>().Points;
            int roll = UnityEngine.Random.Range(0, 3);
            AudioSource.PlayClipAtPoint(clip[roll], Vector3.zero);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
