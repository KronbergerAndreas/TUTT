using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;      

    public float minSpawnY;             
    public float maxSpawnY;             
    private float leftSpawnX;           
    private float rightSpawnX;          

    public float spawnRate;             
    private float lastSpawn;            

    void Start ()
    {
        Camera cam = Camera.main;
        float camWidth = (2.0f * cam.orthographicSize) * cam.aspect;

        leftSpawnX = -camWidth / 2;
        rightSpawnX = camWidth / 2;
    }

    void Update ()
    {
        if(Time.time - spawnRate >= lastSpawn)
        {
            lastSpawn = Time.time;
            SpawnObstacle();
        }
    }

    void SpawnObstacle ()
    {
        GameObject obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)], GetSpawnPosition(), Quaternion.identity);

        obstacle.GetComponent<Obstacle>().moveDir = new Vector3(obstacle.transform.position.x > 0 ? -1 : 1, 0, 0);
    }

    Vector3 GetSpawnPosition ()
    {
        float x = Random.Range(0, 2) == 1 ? leftSpawnX : rightSpawnX;
        float y = Random.Range(minSpawnY, maxSpawnY);

        return new Vector3(x, y, 0);
    }
}