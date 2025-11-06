using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IHealth
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Shoot,
        Dead
    }

    [Header("Settings")]
    public EnemyState currentState = EnemyState.Idle;
    public float chaseRange = 5f;
    public float shootRange = 2.5f;
    public float moveSpeed = 2f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 1f;

    [Header("Health")]
    [SerializeField] private float maxHealth = 50f;
    private float currentHealth;
    private bool isDead = false;

    private Transform player;
    private float shootTimer;

    // IHealth properties
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = maxHealth;
        shootTimer = shootCooldown;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Update state
        if (distanceToPlayer <= shootRange)
            currentState = EnemyState.Shoot;
        else if (distanceToPlayer <= chaseRange)
            currentState = EnemyState.Chase;
        else
            currentState = EnemyState.Idle;

        // Execute state behavior
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
            case EnemyState.Shoot:
                ShootPlayer();
                break;
        }
    }

    void Idle()
    {
        // do nothing
    }

    void ChasePlayer()
    {
        FacePlayer();
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    void ShootPlayer()
    {
        FacePlayer();
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            if (bulletPrefab != null && firePoint != null)
            {
                GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
                
                if (bulletScript)
                {
                    bulletScript.AssignOwnerGameObject(gameObject);
                }
            }
            shootTimer = shootCooldown;
        }
    }

    void FacePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Enemy took {amount} damage. Health now: {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        currentState = EnemyState.Dead;
        Debug.Log("Enemy has died!");
        Destroy(gameObject, 1f);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
