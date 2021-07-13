using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBossBehaviour : MonoBehaviour
{
    EnemyPathfinding pathfinding;
    Animator animator;
    Health health;
    bool isDead;

    [Header("Summoner")]
    [SerializeField] List<GameObject> objToSpawn;
    [SerializeField] float delayForEachSummon;
    bool hasSummoned;

    [Header("Chase")]
    [SerializeField] Transform targetToChase;
    [SerializeField] float chaseDistance = 2;
    [SerializeField] float attackDistance = 1;
    float distanceToTarget;

    [Header("Attack")]
    [SerializeField] float attackDamage = 10;
    [SerializeField] int attackType = 0;
    bool isAttacking;
    [SerializeField] float skillAtkDelay = 4;
    float skillAtkTimer;
    [SerializeField] List<SkillDictionary> dictionary;

    private int currentSkillAttack = 0;

    //kalo amti pindah scene menu awal
    public GameManager ush;
    


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
        if (distanceToTarget < attackDistance)
        {
            float r = Random.Range(0, 100);
            if (r > 65)
            {
                currentSkillAttack = 1;
                Attack(2);
            }
            else
            {
                Attack(0);
            }
                

        }
        else if (distanceToTarget < chaseDistance)
        {
            ChanceOfSkillAttack();

            Move();

        }
        else
        {
            Stop();

        }
    }

    private void ChanceOfSkillAttack()
    {
        if (skillAtkTimer > skillAtkDelay)
        {
            float r = Random.Range(0, 100);
            if (r > 45)
            {
                if(health.CurrentHP/health.MaxHP < 0.3 && !hasSummoned)
                {
                    hasSummoned = true;
                    currentSkillAttack = 0;
                    Attack(1);
                }
                else
                {
                    currentSkillAttack = 1;
                    Attack(2);
                }
            }
            else
            {
                skillAtkTimer = 0;
            }
        }

        skillAtkTimer += Time.deltaTime;
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

    private void SkillAttack(int attackType)
    {
        if (isAttacking) return;
        isAttacking = true;
        UpdateDirection();
        pathfinding.StopMovement(true);
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackType", attackType);
    }

    private void Attack(int attackType)
    {
        if (isAttacking) return;
        isAttacking = true;
        UpdateDirection();
        pathfinding.StopMovement(true);
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
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
        pathfinding.StopMovement(true);
        ush.KembaliMainMenu();
    }

    public void AE_OnAttackEnd()
    {
        isAttacking = false;
        skillAtkTimer = 0;
    }

    public void AE_OnSkillActivate()
    {
       
        if (objToSpawn != null)
        {
            objToSpawn[currentSkillAttack].SetActive(true);
        }
    }
}

[System.Serializable]
public class SkillDictionary
{
    public string skillName;
    public bool canUse = true;
    public bool unlimitedUse = true;
    public int numberOfUse = 1;
    public int attackType = 0;
    public SkillDictionary()
    {
        
    }

    public void UseSkill()
    {
        numberOfUse -= 1;
        if (numberOfUse <= 0 && !unlimitedUse) canUse = false;
    }
}
