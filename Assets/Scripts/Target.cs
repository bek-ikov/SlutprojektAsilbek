using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public static event System.Action<Target> OnAnyTargetDeath;

    private float health = 100f;
    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnAnyTargetDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
