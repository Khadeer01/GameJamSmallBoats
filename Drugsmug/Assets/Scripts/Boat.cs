using UnityEngine;


public class Boat : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject bulletPrefab;

    [Header("Keybinds")]
    [SerializeField] KeyCode shootingKeybind = KeyCode.Space;

    [Header("Transforms")]
    [SerializeField] Transform shootingLocation;

    [Header("Boat Settings")]
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float rotationSpeed = 150.0f;

    [SerializeField] float minVelocity = -5.0f;
    [SerializeField] float maxVelocity = 5.0f;

    [SerializeField] float deceleration = 5.0f;

    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleBoatMovement();
        HandleShooting();
    }
    
    void HandleBoatMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // Rotation input
        float verticalInput = Input.GetAxisRaw("Vertical"); // Moving input

        // Move the boat with forward acceeleration
        if (verticalInput != 0.0f)
        {
            Vector2 movingVector = transform.up * verticalInput * moveSpeed * Time.deltaTime;

            //rb.AddForce(movingVector, ForceMode2D.Force);
            if (rb.linearVelocity.y < minVelocity)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, minVelocity);

            }
            else if (rb.linearVelocity.y > maxVelocity)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxVelocity);
            }
            else
            {
                rb.linearVelocity += movingVector;
            }
        }
        // Add deceleration to the boat when the player is not moving it
        else
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, deceleration * Time.deltaTime);

            // Snap to zero when close enough
            if (rb.linearVelocity.magnitude < 0.05f)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }

        // Rotate the boat
        if (horizontalInput != 0.0f)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;

            Vector3 rotationVector = new Vector3(0.0f, 0.0f, horizontalInput) * rotationAmount;

            transform.Rotate(-rotationVector);
        }
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(shootingKeybind))
        {
            if (bulletPrefab != null)
            {
                if (shootingLocation != null)
                {
                    Instantiate(bulletPrefab, shootingLocation.position, transform.rotation);
                }
                else
                {
                    Instantiate(bulletPrefab, transform.position, transform.rotation);
                }
            }
        }
    }

}
