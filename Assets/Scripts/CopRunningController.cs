using UnityEngine;

public class CopController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float detectionRange = 10f; // Range within which the cop can detect the player
    public float moveSpeed = 4.5f; // Speed at which the cop moves towards the player
    public LayerMask detectionLayer; // LayerMask to specify which layers the raycast should hit
    private float toNewtons = 1000f; // Conversion factor to simulate gravity

    private Rigidbody2D rbody;
    private bool facingRight = false; // Track the cop's facing direction

    void Start()
    {
        // Get the Rigidbody2D component
        rbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Perform a raycast to detect the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRange, detectionLayer);

        //Debug.Log("Raycast hit: " + hit.collider?.name); // Log the name of the object hit by the raycast
        // Check if the raycast hit the player
        if (hit.collider != null && hit.collider.transform == player)
        {
            //Debug.Log("Player detected!");
            // Move the cop towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            rbody.linearVelocity = direction * moveSpeed;

            // Flip the cop's transform based on the player's position
            if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
            {
                Flip();
            }
        }
        else
        {
            // Stop the cop if the player is not detected
            rbody.linearVelocity = Vector2.zero;
        }

        // Apply gravity effect
        rbody.linearVelocity += Vector2.down * toNewtons * Time.deltaTime;
    }

    void Flip()
    {
        // Flip the cop's facing direction
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        // Draw a line to visualize the raycast
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (player.position - transform.position).normalized * detectionRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(1);
            }
        }
    }
}
