using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTameng : MonoBehaviour
{
    EnemyPathfinding pathfinding;
    Animator animator;
    Health health;
    bool isDead;

    [Header("Laser")]
    [SerializeField] List<GameObject> objToSpawn;
    [SerializeField] float delayForEachSummon;
    public LaserAttack laserAtk;
    private int laserType = 0;

    [Header("Chase")]
    [SerializeField] Transform targetToChase;
    [SerializeField] float chaseDistance = 2;
    [SerializeField] float attackDistance = 1;
    float distanceToTarget;

    [Header("Attack")]
    [SerializeField] float attackDamage = 10;
    [SerializeField] int attackType = 0;
    bool isAttacking;



    //Laser laser;
    /*[SerializeField] float skillAtkDelay = 4;
    float skillAtkTimer;*/




    private void Awake()
    {
        UpdateDistance();
        pathfinding = GetComponent<EnemyPathfinding>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        health.OnDeathEvent -= OnDeathEvent;
        health.OnDeathEvent += OnDeathEvent;
    }

    private void UpdateDistance()
    {
        distanceToTarget = Vector2.Distance(transform.position, targetToChase.position);
    }

    private void Update()
    {
        if (isDead) return;
        UpdateDistance();
        /*if (distanceToTarget < attackDistance)
        {
            int tipe = Random.Range(0, objToSpawn.Count);
            laserType = tipe;
            Attack(0);
        }
        else if (distanceToTarget < chaseDistance)
        {
            //ChanceOfSkillAttack();

            Move();

        }
        else
        {
            Stop();

        }*/


        if (distanceToTarget < chaseDistance)
        {
            //ChanceOfSkillAttack();
            if (distanceToTarget < attackDistance)
            {
                float r = Random.Range(0, 100);

                int tipe = (r > 60) ? 1 : 0;
                laserType = tipe;
                Attack(0);
            }

            Move();

        }
        else
        {
            Stop();

        }
    }

    private void Stop()
    {
        animator.SetFloat("Move", 0);
        pathfinding.StopMovement(true);
    }

    private void Move()
    {
        if (isAttacking) return;
        UpdateDirection();
        animator.SetFloat("Move", 1);
        pathfinding.SetTarget(targetToChase);
    }

    private void UpdateDirection()
    {
        float direction = targetToChase.position.x - transform.position.x;
        direction = (int)Mathf.Clamp01((float)direction);
        animator.SetFloat("direction", direction);
    }

    private void Attack(int attackType)
    {
        if (isAttacking) return;
        isAttacking = true;
        laserAtk.OnAttack();
        UpdateDirection();
        //pathfinding.StopMovement(true);
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackType", attackType);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == targetToChase.tag)
        {
            Health health = collision.transform.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);

            }
        }
    }

    public void OnDeathEvent()
    {
        animator.SetTrigger("Die");
        isDead = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        pathfinding.StopMovement(true);
    }

    public void AE_OnLaserEnd()
    {
        laserAtk.OnEndAttack();
        isAttacking = false;
        //skillAtkTimer = 0;
    }

    public void AE_OnLaserActivate()
    {
        Debug.Log("jalan skill");
        if (objToSpawn != null)
        {

            objToSpawn[laserType].SetActive(true);
        }
    }

}
