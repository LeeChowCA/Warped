using UnityEngine;

public class EggTurretAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    private GameObject bulletInst;

    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;
    private EggTurretController eggTurretController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        eggTurretController = GetComponent<EggTurretController>();
    }

    private void Update()
    {
        if (eggTurretController.PlayerWithinRange() && cooldownTimer >= attackCooldown)
        {
            //Debug.Log("attack");
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("shoot");
        cooldownTimer = 0f;

        bulletInst = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Debug.Log("Laser activated at position: " + firePoint.position);
        bulletInst.GetComponent<EggTurretProjectile>().SetSpeed();
    }
}
