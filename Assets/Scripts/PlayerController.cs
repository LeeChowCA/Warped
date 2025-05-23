using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator anim;

    //// movement variables
    [SerializeField] Rigidbody2D rbody;
    private float horizInput;
    private float moveSpeed = 4.0f;
    private float toNewtons = 100f;

    //// jump variables
    private float jumpHeight = 3.0f;        // jump height in units
    private float jumpTimeToApex = 0.375f;  // jump time
    private float initialJumpVelocity;      // upward (Y) velocity at the start of a jump

    bool jumpPressed = false;
    bool isGrounded = false;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask groundLayerMask;
    float groundCheckRadius = 0.3f;

    int jumpMax = 1;
    int jumpAvailable = 0;

    public bool facingRight = true;

    bool shootPressed = false;

    bool isDucking = false;

    private int currentHealth = 1;

    // Collider variables
    private CapsuleCollider2D capsuleCollider;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private Vector2 duckingColliderSize;
    private Vector2 duckingColliderOffset;


    void Start()
    {
        // given a desired jumpHeight and jumpTime, calculate gravity (same formulas as 3D)
        float gravity = (-2 * jumpHeight) / Mathf.Pow(jumpTimeToApex, 2);
        //-42 / -9.81 == 4.28

        rbody.gravityScale = gravity / Physics2D.gravity.y;   // set gravity on Rigidbody

        // calculate jump velocity (upward motion)
        initialJumpVelocity = (2.0f * jumpHeight) / jumpTimeToApex;

        // Get the BoxCollider2D and store its original size and offset
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        originalColliderSize = capsuleCollider.size;
        originalColliderOffset = capsuleCollider.offset;

        // Set the ducking collider size and offset (half the height)
        duckingColliderSize = new Vector2(originalColliderSize.x, originalColliderSize.y / 2);
        duckingColliderOffset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (originalColliderSize.y / 4));
    }



    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180f);
    }

    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");   // read (and store) horizontal input
        bool isRunning = horizInput > 0.01f || horizInput < -0.01f;
        anim.SetBool("isRunning", isRunning);
        //Mathf.Abs(horizInput);

        isDucking = Input.GetAxis("Vertical") < -0.01f;
        anim.SetBool("isDucking", isDucking);

        if (isDucking)
        {
            capsuleCollider.size = duckingColliderSize;
            capsuleCollider.offset = duckingColliderOffset;
        }
        else
        {
            capsuleCollider.size = originalColliderSize;
            capsuleCollider.offset = originalColliderOffset;
        }

        // check if we're grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayerMask) && rbody.linearVelocity.y < 0.01;
        //Debug.Log("Is Grounded: " + isGrounded);
        anim.SetBool("isGrounded", isGrounded);
        if (isGrounded)
        {
            jumpAvailable = jumpMax;
        }


        if (Input.GetButtonDown("Jump") && jumpAvailable > 0)
        {
            jumpPressed = true;
            //Debug.Log("Jump pressed");
        }

        if ((!facingRight && horizInput > 0.01f) || (facingRight && horizInput < -0.01f))
        {
            Flip();
        }

        if (Input.GetButtonDown("Fire1") && !isRunning)
        {
            shootPressed = true;
        }


        // Check if the player has fallen below the ground
        if (transform.position.y < -9)
        {
            Debug.Log("Player fell off the ground!");
            Die(); // Call the Die method to handle death
        }
    }

    private void FixedUpdate()
    {
        // We're moving via Rigidbody physics, so use FixedUpdate
        float xVel = horizInput * moveSpeed * toNewtons * Time.deltaTime;   // determine new x velocity
        float yVel = rbody.linearVelocity.y;

        if (jumpPressed)
        {
            yVel = initialJumpVelocity;
            jumpAvailable--;
            jumpPressed = false;
            anim.SetTrigger("jump");
        }// use existing y velocity

        //if (shootPressed)
        //{
        //    anim.SetTrigger("shoot");
        //    shootPressed = false;
        //}

        Vector3 movement = new Vector2(xVel, yVel);
        rbody.linearVelocity = movement;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }

    public bool canAttack()
    {
        return horizInput == 0 && !isDucking && isGrounded;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("die");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("die");

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false; // Disable all colliders
        }

        // Disable player controls
        this.enabled = false;
        // Notify GameManager
        GameManager.instance.PlayerDied();
    }

    public void EnableControls()
    {
        this.enabled = true; // Enable the PlayerController script
    }

    public void DisableControls()
    {
        this.enabled = false; // Disable the PlayerController script
    }
}