using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meteor : MonoBehaviour
{
    public float ForcePower;
    public int Points;
    public GameObject Meteor_Prefab;
    GameObject ScoreBoard;
    bool isQuitting;
    private void OnEnable()
    {
        ScoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard");
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDisable()
    {
        if (!isQuitting)
        {
            if (transform.localScale.x > 1)
            {
                //Spawn Smaller Meteors and give them a directional force
                Invoke("SpawnObject", 0);
                Invoke("SpawnObject", 0);
            }
        }
    }
    void SpawnObject()
    {
        GameObject SmallerMeteor = Instantiate(Meteor_Prefab, transform.position, Quaternion.identity);
        SmallerMeteor.transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 1);
        SmallerMeteor.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-100, 101), UnityEngine.Random.Range(-100, 101)).normalized * UnityEngine.Random.Range(ForcePower / 2, ForcePower * 2) * Time.deltaTime, ForceMode2D.Impulse);
    }
}
