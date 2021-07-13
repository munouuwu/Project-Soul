using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeLocal : MonoBehaviour
{
    public float flyTime;
    public float damage = 30f;
    public Transform target;
    public float bulletSpeed = 15f;
    public float knockForce = 15f;
    public float rechochetMaxAngle = 45f;
    public bool canRecochet = false;
    public float recochetFlyTimeDivider = 0.1f;

    public int mode = 0;

    public GameObject knife_g;
    public GameObject impaledKnife_g;

    private Rigidbody2D rig;

    public string playerName = "PLAYER_WITCH_01";

    Vector2 lookDir;
    float angle;

    float recochetAngle;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        impaledKnife_g.SetActive(false);
        //target = GameObject.Find(playerName).transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        {
            if(flyTime > 0)
            {
                flyTime -= 1f * Time.deltaTime;
            }
            else
            {
                Impale();
            }
        }

        if(mode == 1)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                mode = 2;
                impaledKnife_g.SetActive(false);
                knife_g.SetActive(true);
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        

        
    }

    private void FixedUpdate()
    {
        if (mode == 2)
        {
            lookDir = new Vector2(target.position.x, target.position.y) - rig.position;
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rig.rotation = angle;
            //rig.velocity = transform.TransformDirection(0, bulletSpeed, 0); 
            rig.velocity = lookDir.normalized * bulletSpeed;
        }
    }

    public void Impale()
    {
        rig.velocity = Vector2.zero;
        rig.angularVelocity = 0;
        mode = 1; //SWITCH TO SLEEP MODE
        impaledKnife_g.SetActive(true); //DISPLAY KNIFE
        impaledKnife_g.transform.rotation = Quaternion.Euler(0, 0, 0); //RECALIBRATE IMPALEDKNIFE ROTATION
        knife_g.SetActive(false); //HIDE KNIFE GRAPHIC
        GetComponent<BoxCollider2D>().enabled = false; //HIDE COLLIDER
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; //STOP THE KNIFE
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(mode == 0)
        {
            *//*if (collision.gameObject.GetComponent<KnifeThrow>() == null)
            {
                Impale();
            }*//*

            if (collision.gameObject.CompareTag("Enemy"))
            {
                Impale();
            }
            
        }else if(mode == 2)
        {
            if(collision.gameObject.GetComponent<KnifeThrow>() != null)
            {
                Destroy(gameObject);
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mode == 0)
        {
            /*if (collision.gameObject.GetComponent<KnifeThrow>() == null)
            {
                Impale();
            }*/

            if (collision.gameObject.CompareTag("Enemy"))
            {
                GameObject enemy = collision.gameObject;
                Health ht = enemy.GetComponent<Health>();
                Rigidbody2D rbs = enemy.GetComponent<Rigidbody2D>();
                if (ht != null && rbs != null)
                {
                    if (FindObjectOfType<AudioManager>() != null)
                    {
                        FindObjectOfType<AudioManager>().Play("Hit Knife");
                    }
                    ht.TakeDamage(damage);
                    rbs.AddForce(rig.velocity.normalized * knockForce, ForceMode2D.Impulse);
                }

            }
            else
            {
                //Fix Biar Ga Kena trigger 
                string ignoredCollision = "Trigger";
                if (collision.tag == ignoredCollision) return;
                //if (collision.tag == "Obstacle") return;
                //End Of Fix
                if (collision.GetComponent<Rigidbody2D>() != null)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(rig.velocity.normalized * knockForce, ForceMode2D.Impulse);
                }

                if (canRecochet)
                {
                    recochetAngle = Mathf.Atan2(-rig.velocity.y, -rig.velocity.x) * Mathf.Rad2Deg - 90f;
                    //rig.rotation = recochetAngle;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, recochetAngle + Random.Range(-rechochetMaxAngle, rechochetMaxAngle)));
                    rig.velocity = transform.TransformDirection(0, bulletSpeed, 0);
                    flyTime = flyTime * recochetFlyTimeDivider;
                }
                else
                {
                    Impale();
                }
            }
            

        }
        else if (mode == 2)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                //Debug.Log("Hit the Enemy");
                GameObject enemy = collision.gameObject;
                Health ht = enemy.GetComponent<Health>();
                Rigidbody2D rbs = enemy.GetComponent<Rigidbody2D>();
                if (ht != null && rbs != null)
                {
                    if (FindObjectOfType<AudioManager>() != null)
                    {
                        FindObjectOfType<AudioManager>().Play("Hit Knife");
                    }
                    ht.TakeDamage(damage);
                    rbs.AddForce(rig.velocity.normalized * knockForce, ForceMode2D.Impulse);
                }
                
            }

            if (collision.gameObject.GetComponent<KnifeThrow>() != null)
            {
                collision.gameObject.GetComponent<KnifeThrow>().AddAmmo();
                Destroy(gameObject);
            }
        }

        
    }
}
