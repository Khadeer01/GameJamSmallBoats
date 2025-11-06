using JetBrains.Annotations;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Values for maxHealth, currentHealth, and checks if the player is dead
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    private bool isDead = false;

    // Gets CurrentHealth and MaxHealth
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    // Sets starting health to max when the game starts
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // Apply damage to the player
    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Player took {amount} damage. They now have {currentHealth} health");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    // Called when the health reaches zero
    public void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Player has died");
    }

    public bool IsDead()
    {
        return isDead;
    }
}
