using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    private GameObject bulletInst;

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

        //Debug.Log("Laser activated at position: " + firePoint.position);
        float direction = playerController.facingRight ? 1f : -1f;
        //Debug.Log("Laser direction: " + direction);
        bulletInst.GetComponent<Projectile>().SetDirectionAndSpeed(direction);
        //Messenger<float>.Broadcast(GameEvent.PlayerAttack, direction);
    }

}

