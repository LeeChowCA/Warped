using UnityEngine;

public class CopController : MonoBehaviour
{
    private Transform playerTransform; // Reference to the player's transform
    public float detectionRange = 10f; // Range within which the cop can detect the player
    public float moveSpeed = 4.5f; // Speed at which the cop moves towards the player
    public LayerMask detectionLayer; // LayerMask to specify which layers the raycast should hit
    private float toNewtons = 1000f; // Conversion factor to simulate gravity

    private Rigidbody2D rbody;
    private bool facingRight = false; // Track the cop's facing direction

    void Awake()
    {
        // Get the Rigidbody2D component
        rbody = GetComponent<Rigidbody2D>();
        playerTransform = GameManager.instance.GetPlayerTransform(); // Get the player's transform from the GameManager
    }

    private void OnEnable()
    {
        GameManager.OnPlayerRespawned += UpdatePlayerTransform; // Subscribe to the event
    }

    private void OnDisable()
    {
        GameManager.OnPlayerRespawned -= UpdatePlayerTransform; // Unsubscribe from the event
    }

    private void UpdatePlayerTransform(Transform newPlayerTransform)
    {
        playerTransform = newPlayerTransform; // Update the playerTransform reference
    }

    void FixedUpdate()
    {
        if (playerTransform == null) return; // Ensure playerTransform is valid

        // Perform a raycast to detect the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTransform.position - transform.position, detectionRange, detectionLayer);

        //Debug.Log("Raycast hit: " + hit.collider?.name); // Log the name of the object hit by the raycast
        // Check if the raycast hit the player
        if (hit.collider != null && hit.collider.transform == playerTransform)
        {
            //Debug.Log("Player detected!");
            // Move the cop towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;
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
        if (playerTransform == null) return; // if it's null, don't draw anything

        // Draw a line to visualize the raycast
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (playerTransform.position - transform.position).normalized * detectionRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit by cop!");
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(1);
            }
        }
    }
}
