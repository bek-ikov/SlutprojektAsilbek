using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Transform player;
    float chaseRange = 30;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < chaseRange)
        {
            // Enemy AI video: https://www.youtube.com/watch?v=b-WZEBLNCik
        }
    }
}
