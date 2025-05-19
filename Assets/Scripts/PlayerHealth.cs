using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Transform projectileTargetPoint;
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
        GameData.finalScore = ScoreManager.Instance.GetScore();
        GameData.finalWave = WaveManager.CurrentWave;
        SceneManager.LoadScene(2);
        // Lägg till proper death state här sen
    }
}
