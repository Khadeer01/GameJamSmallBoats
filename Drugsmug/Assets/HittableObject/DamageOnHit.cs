using UnityEngine;

// Deals damage to any object with IHealth component that collides
public class DamageOnHit : MonoBehaviour
{
    [SerializeField] private float damageAmount = 20f;
    [SerializeField] private bool destroyOnHit = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHealth target = collision.gameObject.GetComponent<IHealth>();  // Find IHealth on the component that collides

        // If found, do damage
        if (target != null)
        {
            target.TakeDamage(damageAmount);
            Debug.Log($"{gameObject.name} dealt {damageAmount} damage to {collision.name}");

            if (destroyOnHit)
            {
                Destroy(gameObject);    // Remove the falling object
            }
        }
    }
}
