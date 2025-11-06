using UnityEngine;

// test script
public class PlayerHealthTester : MonoBehaviour
{
    private IHealth playerHealth;

    [SerializeField] private float damageAmount = 10f;

    private void Start()
    {
        playerHealth = GetComponent<IHealth>();

        if (playerHealth == null)
        {
            Debug.LogError("No IHealth component found");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerHealth?.TakeDamage(damageAmount);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log($"Current Health: {playerHealth?.CurrentHealth}");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log($"Is Dead? {playerHealth?.IsDead()}");
        }
    }
}
