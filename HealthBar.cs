using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public new string tag = "";
    public TMP_Text healthText;
    public Slider healthSlider; 
    private Damageable playerDamageable;

    public void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag(tag);
        if (player == null)
            print("Player/Character game object is not tagged with 'Player' tag in the Unity Editor.");
        
        playerDamageable = player?.GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    void Start()
    {    
        healthSlider.value = CalculateHealthPercentage(playerDamageable.CurrentHealth, playerDamageable.MaxHealth);
        healthText.text = $"Health: {playerDamageable.CurrentHealth} / {playerDamageable.MaxHealth}";
    }

    private float CalculateHealthPercentage(float currentHealth, float maxHealth)
    {
       return currentHealth / maxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }
    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateHealthPercentage(newHealth, maxHealth);
        healthText.text = $"Health: {newHealth} / {maxHealth}";
    }
}
