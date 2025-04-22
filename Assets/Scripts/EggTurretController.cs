using UnityEngine;

public class EggTurretController : MonoBehaviour
{
    private Animator anim;
    private Transform playerTransform;
    public float detectionRange = 10f;
    public LayerMask detectionLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerTransform = GameManager.instance.GetPlayerTransform();
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (PlayerWithinRange())
        {
            //Debug.Log("Player within range");
            SetEggTurretToShoot();
        }
    }

    public bool PlayerWithinRange()
    {


        // Perform a raycast to detect the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTransform.position - transform.position, detectionRange, detectionLayer);
        // Check if the raycast hit the player
        if (hit.collider != null && hit.collider.transform == playerTransform)
        {
            //Debug.Log("Player detected by Egg Turret!");
            // Player detected, trigger the shooting animation
            return true;
        }
        else return false;
    }

    private void SetEggTurretToShoot()
    {
        // This method is called from the animation event
        // You can add your shooting logic here
        //Debug.Log("Egg turret is shooting!");
        anim.SetTrigger("shoot");
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
