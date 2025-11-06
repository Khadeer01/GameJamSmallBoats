using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;
    public float lifetime = 15f;
    public float damage = 10f;

    [Tooltip("The tag of the shooter (Player or Enemy)")]
    public string shooterTag;

    GameObject ownerGameObject = null;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void AssignOwnerGameObject(GameObject owner)
    {
        ownerGameObject = owner;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Apply damage if target has IHealth
        IHealth health = other.GetComponent<IHealth>();
        if (health != null && other.gameObject != ownerGameObject)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
