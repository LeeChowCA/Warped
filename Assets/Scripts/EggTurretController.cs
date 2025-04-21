using UnityEngine;

public class EggTurretController : MonoBehaviour
{
    private Animator anim;
    private Transform playerTransform;
    public float detectionRange = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerTransform = GameManager.instance.GetPlayerTransform();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        // Perform a raycast to detect the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 10f, LayerMask.GetMask("Player"));
        // Check if the raycast hit the player
        if (hit.collider != null)
        {
            // Player detected, trigger the shooting animation
            anim.SetTrigger("shoot");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit by egg turret!");
            // Player entered the trigger area, start detecting player
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(1);
            }

        }
    }

    private void OnDrawGizmos()
    {
        if (playerTransform == null) return; // if it's null, don't draw anything

        // Draw a line to visualize the raycast
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (playerTransform.position - transform.position).normalized * detectionRange);
    }
}
