using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    EnemyPathfinding pathfinder;
    float enemyDistance;
    Animator animator;
    Health health;

    float timer;
    float duration = 2f;

    float timerCharge;
    float maxTimerCharge;
    void Start()
    {
        pathfinder = GetComponent<EnemyPathfinding>();
        animator = GetComponent<Animator>();
        timer = float.PositiveInfinity;
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyDistance = Vector2.Distance(transform.position, pathfinder.targetTransform.position);
        if (enemyDistance < 3)
        {
            //Debug.Log(health.CurrentHP / health.MaxHP);
            if (GetHealthFraction() < 0.5 )
            {

            }
            else
            {
                animator.SetTrigger("Attack");
                pathfinder.StopMovement(true);
                timer = 0;
                
            }
            timerCharge = 0;
            //  0 jika udah big swing
        }
        else if(timer > duration)
        {
            pathfinder.StopMovement(false);
            animator.SetBool("isChase", true);
            timerCharge += Time.deltaTime;
            //pathfinder(MaxFinder)
        } else
        {
            animator.SetBool("isChase", false);
        }

        timer += Time.deltaTime;
        /*if (enemyDistance > 10)
        {
            animator.SetBool("isChase", false);
        }*/
    }

    private float GetHealthFraction()
    {
        return health.CurrentHP / health.MaxHP;
    }
}
