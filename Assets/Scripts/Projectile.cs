using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifetime = 5f;

    private Transform target;

    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    public void SetTarget(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
