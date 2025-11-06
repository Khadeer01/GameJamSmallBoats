using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15.0f;
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
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");


        if (verticalInput != 0.0f)
        {
            Vector2 movingVector = transform.up * verticalInput * moveSpeed * Time.deltaTime;

            //rb.AddForce(movingVector, ForceMode2D.Force);
            if (rb.linearVelocity.y < minVelocity)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, minVelocity);

            }
            else if(rb.linearVelocity.y > maxVelocity)
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
            rb.linearVelocity = Vector2.zero;
            //DecelerateVector(rb.linearVelocity);
            //if (rb.linearVelocity.x != 0.0f || rb.linearVelocity.y != 0.0f)
            //{
            //rb.linearVelocity -= new Vector2(rb.linearVelocity.x - deceleration, rb.linearVelocity.y) * Time.deltaTime;

            //if (rb.linearVelocity.x > 0.0f)
            //{
            //    rb.linearVelocity -= new Vector2(deceleration, 0.0f);
            //}
            //else if (rb.linearVelocity.x < 0.0f)
            //{
            //    rb.linearVelocity += new Vector2(deceleration, 0.0f);
            //}

            //if (rb.linearVelocity.y > 0.0f)
            //{
            //    rb.linearVelocity -= new Vector2(0.0f, deceleration);
            //}
            //else if (rb.linearVelocity.y < 0.0f)
            //{
            //    rb.linearVelocity += new Vector2(0.0f, deceleration);
            //}
            //rb.linearVelocity -= deceleration;
            //if (rb.linearVelocity.y > 0.0f)
            //{
            //    rb.linearVelocity -= new Vector2(0.0f, deceleration) * Time.deltaTime;

            //}
            //else
            //{
            //    rb.linearVelocity = Vector2.zero;
            //}
        }

        // Rotate the boat
        if (horizontalInput != 0.0f)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;

            Vector3 rotationVector = new Vector3(0.0f, 0.0f, horizontalInput) * rotationAmount;
            
            transform.Rotate(-rotationVector);
        }
    }

}
