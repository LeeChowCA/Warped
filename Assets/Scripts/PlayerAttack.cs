using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] lasers;

    private Animator anim;
    private PlayerController playerController;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && cooldownTimer >= attackCooldown && playerController.canAttack())
        {
            Debug.Log("attack");
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("shoot");
        cooldownTimer = 0f;

        lasers[FindFireball()].transform.position = firePoint.position;
        //lasers[0].SetActive(true); // Ensure the laser is active
        Debug.Log("Laser activated at position: " + firePoint.position);
        float direction = playerController.facingRight ? 1f : -1f;
        Debug.Log("Laser direction: " + direction);
        lasers[FindFireball()].GetComponent<Projectile>().SetDirection(direction);
    }

    private int FindFireball()
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            if (!lasers[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}

