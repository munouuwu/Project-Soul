using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    
	public Transform bar;

	public Animator animator;

	public GameObject minibossPrefab;

	Health health;
    EnemyAI pathfinding;
	public int maxHealth;

	private Vector2 direction;
	float previousX;
	public float enemyDistance;
	private bool minionSpawned = false;

	public GameObject portalSummon;

	void Start()
	{
		health = gameObject.GetComponent<Health>();
		pathfinding = GetComponent<EnemyAI>();
		maxHealth = (int)health.CurrentHP;

		health.OnDamageEvent -= OnKenaDamage;
		health.OnDamageEvent += OnKenaDamage;
		previousX = transform.position.x;

		if (portalSummon)
		{
            //bar.gameObject.SetActive(false);
            pathfinding.stopTest = true;
            //Instantiate(portalSummon, transform.position, transform.rotation);
            StartCoroutine(OnSummon());
		}
	}

    IEnumerator OnSummon()
    {
        Instantiate(portalSummon, transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        pathfinding.stopTest = false;
        //bar.gameObject.SetActive(true);
        yield return null;
    }

	void Update()
	{
		bar.localScale = new Vector3(1.0f * health.CurrentHP / maxHealth, bar.localScale.y, bar.localScale.z);

		if (health.IsDead)
		{
			Die();
		}

		direction = gameObject.GetComponent<EnemyAI>().directionTele;
		
		// Set Flip Parameter
		if ((transform.position.x - previousX) < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
			animator.SetBool("isFlip", true);
		}
		else if((transform.position.x - previousX) > 0)
        {
			GetComponent<SpriteRenderer>().flipX = false;
			animator.SetBool("isFlip", false);
		}

		previousX = transform.position.x;

		// Find Enemy Distance

		enemyDistance = Vector2.Distance(transform.position, gameObject.GetComponent<EnemyAI>().target.position);

		if (enemyDistance < 3)
		{
			//Debug.Log(health.CurrentHP / health.MaxHP);

			if (health.CurrentHP/health.MaxHP < 0.5) {
				// Change Attack Stance
				animator.SetTrigger("Attack2");

				if (health.CurrentHP / health.MaxHP < 0.4)
                {
					animator.SetTrigger("Summon");
				}

			}
			else {
				animator.SetTrigger("Attack");
			}
		}
		else
		{
			animator.SetBool("isChase", true);
		}

		if (enemyDistance > 10)
		{
			animator.SetBool("isChase", false);
		}

		if (health.CurrentHP / health.MaxHP < 0.3)
		{
			if (minibossPrefab)
			{
				if (minionSpawned == false)
				{
					Instantiate(minibossPrefab, transform.position, transform.rotation);
					Instantiate(minibossPrefab, transform.position, transform.rotation);
					Instantiate(minibossPrefab, transform.position, transform.rotation);
					Instantiate(minibossPrefab, transform.position, transform.rotation);
					Instantiate(minibossPrefab, transform.position, transform.rotation);

					minionSpawned = true;
				}
			}
		}

	}

	public void OnKenaDamage(float damage)
	{
		animator.SetTrigger("Hurt");
	}

	void Die()
	{
		//Debug.Log("Enemy died!");

		// Die animation
		animator.SetBool("isDead", true);

		// Disable the enemy
		GetComponent<Collider2D>().enabled = false;
		this.enabled = false;

		//hilang deh
		this.gameObject.SetActive(false);
	}

    
}
