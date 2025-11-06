using UnityEngine;

public interface IHealth
{
    // Getters for CurrentHealth and MaxHealth values
    float CurrentHealth { get; }
    float MaxHealth { get; }

    // Called when taking damage
    void TakeDamage(float amount);

    // Called when health reaches to zero
    void Die();

    // Returns true if the object is dead
    bool IsDead();
}
