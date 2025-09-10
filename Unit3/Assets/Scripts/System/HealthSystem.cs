using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] private float maxHealth;

    public Action OnDeath;
    public Action<float> OnHealthChanged;

    public bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
       
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth);

    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth);

        if(currentHealth <= 0 && !isDead)
        {
            isDead = true;
            OnDeath?.Invoke();
        }
    }
}
