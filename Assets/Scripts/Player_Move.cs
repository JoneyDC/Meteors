using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject projectile, GameOverCanvas;
    public Transform ShotPoint;
    public float Speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the player based on input
        Movement();
        //Spawns a projectile with a movememnt script at the selected position
        Shooting();

    }
    void Movement()
    {
        if (Input.GetButton("Forward"))
        {
            rb.AddForce(transform.right * Speed * Time.deltaTime);
        }
        if(Input.GetButton("Left"))
        {
            transform.Rotate(0, 0, 1);
        }
        if (Input.GetButton("Right"))
        {
            transform.Rotate(0, 0, -1);
        }
    }
    void Shooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile, ShotPoint.position, transform.rotation);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Meteor"))
        {
            GameOverCanvas.SetActive(true);
            Destroy(gameObject);
        }
    }
}
