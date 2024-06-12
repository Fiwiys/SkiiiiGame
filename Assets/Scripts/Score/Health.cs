using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar;
    public TextMeshProUGUI healthText; 
    public float healthAmount = 100f;
    public float maxHealth = 100f;
    private Timer timer;

    private void Start()
    {
        healthAmount = maxHealth;
        
        UpdateHealthText();
        UpdateHealthBar();
        
        timer = FindObjectOfType<Timer>();
        
        if (timer != null)
        {
            timer.currentHealth = healthAmount;
        }
    }

    private void Update()
    {
        if (healthAmount <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        UpdateHealthBar();
        
        if (HighScore.Instance != null)
        {
            HighScore.Instance.UpdateLeaderboardUI();
        }
        
        UpdateHealthText();
        
        if (timer != null)
        {
            timer.currentHealth = healthAmount;
        }
    }

    private void UpdateHealthText()
    {
        healthText.text = Mathf.Max(healthAmount, 0).ToString();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = healthAmount / maxHealth;
    }
}
