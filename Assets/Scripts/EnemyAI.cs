using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Transform player;
    public int attackDamage = 20;
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
        if (distance > attackRange)
        {
            // Grounded movement
            Vector3 direction = player.position - transform.position;
            direction.y = 0f; // Ignorera vertikala skillnaden
            direction = direction.normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;

            // Kolla på spelaren horisontellt
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);
        }
        else if (distance <= attackRange)
        {
            // Attackera spelaren
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
        playerHealth.TakeDamage(attackDamage);
    }

            // Enemy AI video: https://www.youtube.com/watch?v=b-WZEBLNCik
}

