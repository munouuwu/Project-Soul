using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float damage = 10f;
    public Transform target;
    public bool seekerMode;
    public float turnRate = 5f;
    public float bulletSpeed;

    public float flyTime = 3f;
    

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (seekerMode)
        {
            if (flyTime > 0)
            {
                flyTime -= 1f * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (seekerMode)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f), turnRate * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(0, bulletSpeed, 0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Debug.Log("Hit the " + collision.name);*/

        if (collision.tag == "Trigger") return;
        if (collision.tag == "Bullet") return;

        if (collision.CompareTag("Player"))
        {
            Debug.Log("HIT THE PLAYER");
            collision.GetComponent<Health>().TakeDamage(damage);
            
        }
        Destroy(gameObject);

    }
}
