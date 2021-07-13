using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMeleeatk : MonoBehaviour
{

    //deteksi musuh
    public LayerMask whatIsEnemies;

    public UltimateSystem ultimate;
    //untuk animasi
    public Animator playerAnimator;

    //untuk bagian attack make mouse
    public Transform attackPos;
    public float attackRange;
    public int attackDamage = 10;

    public int combo;
    public bool attack;

    private Rigidbody2D rig;
    public float knockForce;

    //public float majuDikit = 1f;

    //public int UltiCounter;

    void Start()
    {
        ultimate = GetComponent<UltimateSystem>();
        playerAnimator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Combos_();
    }
    public void Combos_()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !attack)
        {
            Attack();

        }
    }

    public void Attack()
    {
        if (attack) return;
        attack = true;
        playerAnimator.SetTrigger("" + combo);

        //detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

        //damaging enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(attackDamage);
            enemy.GetComponent<Rigidbody2D>().AddForce(rig.velocity.normalized * knockForce, ForceMode2D.Impulse);
            ultimate.FillUltimate(1);
            //UltiCounter = Mathf.Min(UltiCounter + 1, 5);
        }

        //Debug.Log("ulticounter " + UltiCounter);
    }

    public void Start_Combo()
    {
        attack = false;

        if (combo < 3)
        {
            combo++;
        }
    }

    public void Finish_Ani()
    {
        attack = false;
        combo = 0;
    }
    void OnDrawGizmosSelected()
    {
        if (attackPos == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void FixedUpdate()
    {
        PlayerAnimationAttack();
    }

    private void PlayerAnimationAttack()
    {
        //get mousePosition
        Vector3 posisiMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //jarak mouse ke player
        Vector3 jarakMouseKePlayer = posisiMouse - transform.position;

        playerAnimator.SetFloat("Attack_X", jarakMouseKePlayer.x);
        playerAnimator.SetFloat("Attack_Y", jarakMouseKePlayer.y);

        /*//maju dikit
        
        Vector2 PlayerMajuDikit = new Vector2(jarakMouseKePlayer.x * majuDikit, jarakMouseKePlayer.y * majuDikit);
        rig.velocity = PlayerMajuDikit.normalized;*/
        
    }


}
