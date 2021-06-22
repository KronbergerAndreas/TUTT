using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 moveDir;         
    public float moveSpeed;         

    private float aliveTime = 8.0f; 

    void Start ()
    {
        Destroy(gameObject, aliveTime);
    }

    void Update ()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        transform.Rotate(Vector3.back * moveDir.x * (moveSpeed * 20) * Time.deltaTime);
    }
}