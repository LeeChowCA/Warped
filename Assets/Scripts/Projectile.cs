using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private float lifeTime; // Time before the projectile is deactivated

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit)
            return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if(lifeTime > 5) gameObject.SetActive(false); // Deactivate the projectile after 5 seconds
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("explode");

            Destroy(collision.gameObject); // Destroy the enemy
        }
    }

    public void SetDirection(float _direction) {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false; 
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate() { 
        Debug.Log("Deactived");
        gameObject.SetActive(false);
    }
}
