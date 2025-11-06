using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;
    public float lifetime = 3f;
    public float damage = 10f;

    [Tooltip("The tag of the shooter (Player or Enemy)")]
    public string shooterTag;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Don't hit the shooter or friendly units
        if (other.CompareTag(shooterTag))
            return;

        // Apply damage if target has IHealth
        IHealth health = other.GetComponent<IHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            // Destroy bullet if it hits something non-living (like walls)
            Destroy(gameObject);
        }
    }
}
