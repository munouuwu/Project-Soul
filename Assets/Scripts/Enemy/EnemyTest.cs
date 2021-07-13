using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
	
	public Transform bar;

	public Animator animator;

	Health health;

	public int maxHealth;

    void Start()
    {
		health = gameObject.GetComponent<Health>();
		maxHealth = (int)health.CurrentHP;

		health.OnDamageEvent -= OnKenaDamage;
		health.OnDamageEvent += OnKenaDamage;
    }

    void Update()
    {
		bar.localScale = new Vector3(1.0f * health.CurrentHP / maxHealth, bar.localScale.y, bar.localScale.z);

		if (health.CurrentHP <= 0)
		{
			Die();
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
