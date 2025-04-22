using UnityEngine;

public class EggTurretProjectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed;
    private bool hit;
    private float direction;

    private Animator anim;
    private BoxCollider2D boxCollider;

    [SerializeField] private float lifeTime = 3f; // Time before the projectile is deactivated
    private Rigidbody2D rb;

    [SerializeField] private LayerMask whatDestroysBullet;


    private void Awake()
    {
        //Messenger<float>.AddListener(GameEvent.PlayerAttack, SetDirectionAndSpeed);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SetDestroyTime();
    }

    private void Update()
    {

    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //anim.SetTrigger("explode");
        Debug.Log("explode");
        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {

            Destroy(gameObject);

        } // destroy the projectile if it hits an object in the whatDestroysBullet layer

        if (collision.gameObject.CompareTag("Player"))
        {
            //anim.SetTrigger("explode");
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(1);
            }
        }
    }
     
    public void SetSpeed()
    {
        rb.linearVelocity = new Vector2(speed, 0);
        Debug.Log( "speed is " + speed);
    }


    private void Deactivate()
    {
        Debug.Log("Deactived");
        gameObject.SetActive(false);
    }
}
