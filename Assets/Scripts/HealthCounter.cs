using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    public PlayerHealth playerHealth;

    [SerializeField]
    private TextMeshProUGUI healthCounter;

    void Update()
    {
        if (playerHealth.currentHealth > 0)
        {
            healthCounter.text = $"{playerHealth.currentHealth}/{playerHealth.maxHealth}";
        }
        else
        {
            healthCounter.text = "Spelare död";
        }
    }
}
