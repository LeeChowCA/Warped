using UnityEngine;

public class Projectile : MonoBehaviour
{
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
        Debug.Log(direction + "in awake");
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
        anim.SetTrigger("explode");
        Debug.Log("explode");
        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            
            Destroy(gameObject);
            
        } // destroy the projectile if it hits an object in the whatDestroysBullet layer

        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("explode");
            Destroy(collision.gameObject); // Destroy the enemy
            Messenger.Broadcast(GameEvent.ENEMY_DEAD);
        }
    }

    public void SetDirectionAndSpeed(float _direction)
    {
        direction = _direction;
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * direction; // Flip the sprite if needed
        transform.localScale = localScale;

        Debug.Log(speed);
        Debug.Log(direction);
        rb.linearVelocity = new Vector2(direction * speed, 0);
    }


    //private void Awake()
    //{
    //    anim = GetComponent<Animator>();
    //    boxCollider = GetComponent<BoxCollider2D>();
    //    Debug.Log(boxCollider);

    //    if (boxCollider == null)
    //    {
    //        Debug.LogError("BoxCollider2D is not attached to the Projectile prefab!");
    //    }
    //}

    //private void Update()
    //{
    //    if (hit)
    //        return;
    //    float movementSpeed = speed * Time.deltaTime * direction;
    //    transform.Translate(movementSpeed, 0, 0);

    //    lifeTime += Time.deltaTime;
    //    if(lifeTime > 5) gameObject.SetActive(false); // Deactivate the projectile after 5 seconds
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        hit = true;
    //        boxCollider.enabled = false;
    //        anim.SetTrigger("explode");

    //        Destroy(collision.gameObject); // Destroy the enemy
    //    }
    //}

    //public void SetDirection(float _direction) {
    //    lifeTime = 0;
    //    direction = _direction;
    //    gameObject.SetActive(true);
    //    hit = false; 
    //    boxCollider.enabled = true;

    //    float localScaleX = transform.localScale.x;
    //    if (Mathf.Sign(localScaleX) != _direction) {
    //        localScaleX = -localScaleX;
    //    }

    //    transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    //}

    private void Deactivate()
    {
        Debug.Log("Deactived");
        gameObject.SetActive(false);
    }
}
