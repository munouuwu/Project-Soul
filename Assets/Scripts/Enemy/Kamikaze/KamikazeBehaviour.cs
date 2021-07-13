using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeBehaviour : MonoBehaviour
{
	public Transform bar;

	public Animator animator;

	/*public GameObject minibossPrefab;*/

	Health health;
    

	public int maxHealth;

	private Vector2 direction;
	float previousX;
	public float enemyDistance;


    [Header("Kamikaze")]
	private bool isExplode = false;
    public float explodingRange = 2;
    [SerializeField] GameObject explosionPrefab;
    ThrowableObject throwable;
    private float timerAfterThrown = 0;
    public float delayAfterThrown = 1.5f;

    public bool stopMovement;

    EnemyPathfinding pathfinding;

    bool tickingBomb;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explodingRange);
    }

    void Start()
	{
        pathfinding = GetComponent<EnemyPathfinding>();
        throwable = GetComponent<ThrowableObject>();

		health = gameObject.GetComponent<Health>();
		maxHealth = (int)health.CurrentHP;

		health.OnDamageEvent -= OnKenaDamage;
		health.OnDamageEvent += OnKenaDamage;
		previousX = transform.position.x;
	}

	void Update()
	{
        if(tickingBomb)
        {
            timerAfterThrown += Time.deltaTime;
            if(timerAfterThrown > delayAfterThrown)
            {
                Explode();
            }
        }

        if (stopMovement)
        {
            
            return;
        }
		bar.localScale = new Vector3(1.0f * health.CurrentHP / maxHealth, bar.localScale.y, bar.localScale.z);

		if (health.IsDead)
		{
			Die();
		}

		//direction = gameObject.GetComponent<EnemyAI>().directionTele;

		// Set Flip Parameter
		if ((transform.position.x - previousX) < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
			animator.SetBool("isFlip", true);
		}
		else if ((transform.position.x - previousX) > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
			animator.SetBool("isFlip", false);
		}

		previousX = transform.position.x;

		// Find Enemy Distance

		enemyDistance = Vector2.Distance(transform.position, pathfinding.targetTransform.position);

		if (enemyDistance < explodingRange)
		{
            if (isExplode == false)
            {
                //Explode();
                StartExplode();
            }
            else
            {

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

		/*if (animator.GetCurrentAnimatorStateInfo(0).IsName("KamikazeAnimExplosion"))
		{
			GetComponent<Collider2D>().isTrigger = true;
			Debug.Log("Enemy died!");
			Die();
		}
*/
        
        //timerAfterThrown += Time.deltaTime;

	}

    private void StartExplode()
    {
        
        tickingBomb = true;
        animator.SetTrigger("Kamikaze");
    }

    private void Explode()
    {
        if (explosionPrefab != null)
        {
            if (FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play("Explosion");
            }
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
        //gameObject.GetComponent<EnemyAI>().speed = 1f;
        GetComponent<Collider2D>().enabled = true;
    }
    /*private void Explode()
    {
        //animator.SetTrigger("Kamikaze");
        if (!throwable.isPulled && timerAfterThrown > delayAfterThrown)
        {
            isExplode = true;
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }   
            gameObject.SetActive(false);
            //gameObject.GetComponent<EnemyAI>().speed = 1f;
            GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            timerAfterThrown = 0;
        }
        
    }*/

    public void OnKenaDamage(float damage)
	{
		if (isExplode == false)
		{
			animator.SetTrigger("Hurt");
		}

        
	}

    /*private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player" || collision.tag == "Enemy")
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
	}*/

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
