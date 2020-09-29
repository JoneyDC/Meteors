using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGenerator : MonoBehaviour
{
    public float DifficultyIncreaseSpeed;
    public float StartingSpawnTime;
    public float StartingIntensity;
    public int MaxMeteors;
    [Space]
    public List<GameObject> Meteor;

    float intensity,spawnTimer;
    bool IntensityStablisation, MaxIntensity;
    void Start()
    {
        if (!MaxIntensity)
        {
            intensity = StartingIntensity;
        }
        if (GameObject.FindGameObjectsWithTag("Meteor").Length < MaxMeteors)
        {
            spawnTimer = StartingSpawnTime / intensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        intensity += Time.deltaTime * DifficultyIncreaseSpeed;
        if(intensity > 3 && !IntensityStablisation)
        {
            DifficultyIncreaseSpeed = DifficultyIncreaseSpeed / 2;
            IntensityStablisation = true;
        }
        //Cap Intensity if spawn time is less than 0.67s
        if(intensity > StartingSpawnTime * 1.5f)
        {
            MaxIntensity = true;
        }
    }
    private void FixedUpdate()
    {
        //Spawn and Meteor every few seconds with randomly generated power
        if(spawnTimer > StartingSpawnTime/intensity)
        {
            int Roll = Random.Range(0, Meteor.Count);
            GameObject Spawn = Instantiate(Meteor[Roll], new Vector3(Random.Range(Screen.width, Screen.width + 1),Random.Range(0, Screen.height)), Quaternion.identity);
            Spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-100, 101), UnityEngine.Random.Range(-100, 101)).normalized * Random.Range(Spawn.GetComponent<Meteor>().ForcePower / 2, Spawn.GetComponent<Meteor>().ForcePower * 2) * Time.deltaTime * intensity, ForceMode2D.Impulse);
            
            spawnTimer = 0f;
        }
    }
}
