using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image heartImage;

    public Sprite heartSprite;
    public Sprite skullSprite;

    [SerializeField]
    private TextMeshProUGUI healthCounter;

    void Update()
    {
        if (playerHealth.currentHealth > 0)
        {
            heartImage.sprite = heartSprite;
            healthCounter.text = $"{playerHealth.currentHealth}/{playerHealth.maxHealth}";
        }
        else
        {
            heartImage.sprite = skullSprite;
            healthCounter.text = "Spelare död";
        }
    }
}
