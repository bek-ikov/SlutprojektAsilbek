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
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
        else
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

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