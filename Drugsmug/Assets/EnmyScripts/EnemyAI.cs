using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Shoot
    }

    [Header("Settings")]
    public EnemyState currentState = EnemyState.Idle;
    public float chaseRange = 5f;       // Start chasing when player is within this range
    public float shootRange = 2.5f;     // Start shooting when player is within this range
    public float moveSpeed = 2f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 1f;

    private Transform player;
    private float shootTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        shootTimer = shootCooldown;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Update state based on distance
        if (distanceToPlayer <= shootRange)
            currentState = EnemyState.Shoot;
        else if (distanceToPlayer <= chaseRange)
            currentState = EnemyState.Chase;
        else
            currentState = EnemyState.Idle;

        // Perform behavior based on state
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
        
    }

    void ChasePlayer()
    {
        FacePlayer();

        // Move toward player
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    void ShootPlayer()
    {
        FacePlayer(); // keep facing player while shooting

        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            if (bulletPrefab != null && firePoint != null)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }

            shootTimer = shootCooldown; // reset cooldown
        }
    }

    void FacePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
