using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicExplosion : MonoBehaviour
{
    private float explosionSize = 0.76f;
    public float damage = 10;
    private CircleCollider2D col;
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        StartCoroutine(Explode());  
    }

    private void OnEnable()
    {
        col = GetComponent<CircleCollider2D>();
        StartCoroutine(Explode());
    }

    private void OnTriggerEnter2D(Collider2D collision) // benda memiliki collider bakal dibaca dalam bentuk collision
    {
        Debug.Log("YANG KENA ---" + collision.gameObject.name);
        Health health = collision.GetComponent<Health>();
        if (health == null) return;
        health.TakeDamage(damage);
        Debug.Log("Ada yang kena leadakan=" + health.gameObject.name);
        //Debug.Log(collision.name + "take damage 10");
    }



    private IEnumerator Explode()
    {
        /*float currentRadius = col.radius;*/
        while (col.radius < explosionSize)
        {
            col.radius = Mathf.Max(explosionSize, col.radius + explosionSize * Time.deltaTime);

            yield return null;
        }

        //yield return new WaitForSeconds(3f);

        //Destroy(gameObject);
    }
    public void AE_OnAnimationEnd()
    {
        Destroy(gameObject);
    }



    
}
