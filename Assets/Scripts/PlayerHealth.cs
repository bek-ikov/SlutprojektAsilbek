using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Spelaren tog skada. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            deathState();
        }
    }
    void deathState()
    {
        Debug.Log("Spelaren har dött!");
        // Lägg till proper death state här sen
    }
}
