using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    Transform player;
    public float attackRange = 20f;
    public float moveSpeed = 4f;
    public float shootCooldown = 2f;
    private float lastShotTime;

    public GameObject projectilePrefab;
    public Transform shootPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            // Ground movement
            Vector3 direction = player.position - transform.position;
            direction.y = 0f; // Ignorera vertikala skillnaden
            direction = direction.normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;

            // Kolla på spelaren horisontellt
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            // Kolla på spelaren horisontellt även när stilla
            Vector3 lookDirection = player.position - transform.position;
            lookDirection.y = 0f;
            if (lookDirection != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(lookDirection);

            if (Time.time > lastShotTime + shootCooldown)
            {
                lastShotTime = Time.time;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && shootPoint != null && player != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

            Transform targetPoint = player.GetComponent<PlayerHealth>().projectileTargetPoint;
            if (targetPoint == null)
            {
                // Om ingen targetPoint hittas, fallback till spelarens default pos
                targetPoint = player;
            }

            Vector3 direction = (targetPoint.position - shootPoint.position).normalized;

            projectile.GetComponent<Projectile>().SetTarget(direction);
        }
    }
}