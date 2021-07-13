using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossController : MonoBehaviour
{

    EnemyPathfinding pathfinder;
    float enemyDistance;
    Animator animator;
    Health health;

    float timer;
    float duration = 2f;

    
    float timerCharge;
    float maxTimerCharge;
    //Summoning
    bool summoning;
    [SerializeField]
    SpawnEnemySkill enemySkill;
    void Start()
    {
        pathfinder = GetComponent<EnemyPathfinding>();
        animator = GetComponent<Animator>();
        timer = float.PositiveInfinity;
        health = GetComponent<Health>();

        health.OnDamageEvent -= OnDamage;
        health.OnDamageEvent += OnDamage;
        health.OnDeathEvent -= OnDeath;
        health.OnDeathEvent += OnDeath;
    }

    private void OnDamage(float damage)
    {
        animator.SetBool("Hurt", true);
    }

    private void OnDeath()
    {
        pathfinder.StopMovement(true);
        animator.SetTrigger("Dead");
        GetComponent<CapsuleCollider2D>().enabled = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (health.IsDead) return;

        enemyDistance = Vector2.Distance(transform.position, pathfinder.targetTransform.position);
        if(enemyDistance > 15)
        {
            animator.SetBool("isChase", false);
        }
        else if (enemyDistance < 3)
        {
            //Debug.Log(health.CurrentHP / health.MaxHP);
            if (GetHealthFraction() < 0.5 && !summoning)
            {
                Debug.Log("Summon");
                summoning = true;
                enemySkill.SummonEnemy();
                timer = 0;
            }
            else
            {
                Debug.Log("Attack");
                animator.SetTrigger("Attack");
                pathfinder.StopMovement(true);
                timer = 0;
            }
            timerCharge = 0;
            //  0 jika udah big swing
        }
        else if (timer > duration)
        {
            pathfinder.StopMovement(false);
            animator.SetBool("isChase", true);
            //timerCharge += Time.deltaTime;
            //pathfinder(MaxFinder)
        }
        
        

        timer += Time.deltaTime;
    }

    private float GetHealthFraction()
    {
        float temp = health.CurrentHP / health.MaxHP;
        Debug.Log(temp);
        return temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.GetComponent<Health>().TakeDamage(10);

            Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
            Vector2 vec;

            //sesuai Arah Musuh
            if (rb.velocity == Vector2.zero)
            {
                vec = Vector2.right;
            }
            else
            {
                vec = rb.velocity.normalized;
            }
            vec = vec * 4000;

            rb.AddForce(vec);
        }
    }
}