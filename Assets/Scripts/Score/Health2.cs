using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health2 : MonoBehaviour
{
    public Image healthBar;
    public TextMeshProUGUI healthText; 
    public float healthAmount = 100f;
    public float maxHealth = 100f;
    private Timer2 timer;

    private void Start()
    {
        healthAmount = maxHealth;
        
        UpdateHealthText();
        UpdateHealthBar();
        
        timer = FindObjectOfType<Timer2>();
        
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
        
        if (HighScore2.Instance != null)
        {
            HighScore2.Instance.UpdateLeaderboardUI();
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
