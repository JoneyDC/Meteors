using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
    public bool Mobile;
    [Space]
    Rigidbody2D rb;
    public GameObject projectile, GameOverCanvas;
    public Transform ShotPoint;
    public float Speed;
    public AudioClip GameOver, Booster;
    [SerializeField] GameObject HighScore, ScoreBoard;
    float shotcooldown, AudioTimer;
    bool RotatingLeft, RotatingRight, MovingForward;
    public GameObject Button1, Button2, Button3, Button4;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shotcooldown = 1f;
        if(!Mobile)
        {
            Button1.SetActive(false);
            Button2.SetActive(false);
            Button3.SetActive(false);
            Button4.SetActive(false);
        }
        else
        {
            Button1.SetActive(true);
            Button2.SetActive(true);
            Button3.SetActive(true);
            Button4.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the player based on input
        Movement();
        //Spawns a projectile with a movememnt script at the selected position
        Shooting();
        shotcooldown += Time.deltaTime;
        AudioTimer += Time.deltaTime;

    }
    public void RotateLeft()
    {
        RotatingLeft = true;
    }
    public void RotateRight()
    {
        RotatingRight = true;
    }
    public void StopRotateLeft()
    {
        RotatingLeft = false;
    }
    public void StopRotateRight()
    {
        RotatingRight = false;
    }
    public void MoveForward()
    {
        MovingForward = true;
    }
    public void StopMoveForward()
    {
        MovingForward = false;
    }
    public void Fire()
    {
        Instantiate(projectile, ShotPoint.position, transform.rotation);
        GetComponent<AudioSource>().Play();
        shotcooldown = 0f;
    }
    void Movement()
    {
        if (Input.GetButton("Forward"))
        {
            rb.AddForce(transform.right * Speed * Time.deltaTime);
            if (AudioTimer > 0.1f)
            {
                AudioSource.PlayClipAtPoint(Booster, Vector3.zero);
                AudioTimer = 0f;
            }
        }

        transform.Rotate(0, 0, Input.GetAxisRaw("Horizontal"));
        //Mobile Input
        if (RotatingLeft)
        {
            transform.Rotate(0, 0, 1);
        }
        if (RotatingRight)
        {
            transform.Rotate(0, 0, -1);
        }
        if(MovingForward)
        {
            rb.AddForce(transform.right * Speed * Time.deltaTime);
            if (AudioTimer > 0.1f)
            {
                AudioSource.PlayClipAtPoint(Booster, Vector3.zero);
                AudioTimer = 0f;
            }
        }
       
    }
    void Shooting()
    {
        if (Input.GetButtonDown("Fire1") && shotcooldown > 0.1f && !Mobile)
        {
            Instantiate(projectile, ShotPoint.position, transform.rotation);
            GetComponent<AudioSource>().Play();
            shotcooldown = 0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Meteor"))
        {
            HighScore.SetActive(true);
            HighScore.SetActive(false);
            GameOverCanvas.SetActive(true);
            HighScore.GetComponent<HighscoreTable>().AddHighscoreEntry(ScoreBoard.GetComponent<ScoreSystem>().Score, GameObject.FindGameObjectWithTag("NameStore").GetComponent<Name_Store>().Name);
            GameObject.FindGameObjectWithTag("NameStore").GetComponent<AudioSource>().Stop();
            AudioSource.PlayClipAtPoint(GameOver, Vector3.zero);
            Button1.SetActive(false);
            Button2.SetActive(false);
            Button3.SetActive(false);
            Button4.SetActive(false);
            Destroy(gameObject);
        }
    }
}
