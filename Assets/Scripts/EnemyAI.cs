using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Transform player;
    public float chaseRange = 30f;
    public float attackRange = 2f;
    public float moveSpeed = 5f;
    public float attackCooldown = 1.5f;
    public float lastAttackTime;

    private PlayerHealth playerHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < chaseRange && distance > attackRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            // Enemy AI video: https://www.youtube.com/watch?v=b-WZEBLNCik
        }
        else if (distance <= attackRange)
        {
            // Attack the player
            if (Time.time > lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                AttackPlayer();
            }
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Enemy attacks player!");
        playerHealth.TakeDamage(10);
    }

}

