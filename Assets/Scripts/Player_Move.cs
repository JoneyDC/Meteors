using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject projectile, GameOverCanvas;
    public Transform ShotPoint;
    public float Speed;
    public AudioClip GameOver, Booster;
    [SerializeField] GameObject HighScore, ScoreBoard;
    float shotcooldown, AudioTimer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shotcooldown = 1f;
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
        /*
        if (Input.GetButton("Left"))
        {
            transform.Rotate(0, 0, 1);
        }
        if (Input.GetButton("Right"))
        {
            transform.Rotate(0, 0, -1);
        }
        */
    }
    void Shooting()
    {
        if (Input.GetButtonDown("Fire1") && shotcooldown > 0.1f)
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
            Destroy(gameObject);
        }
    }
}
