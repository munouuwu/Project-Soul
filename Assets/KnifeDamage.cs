using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDamage : MonoBehaviour
{
    public float Damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Enemy") return;
        Health hth = collision.transform.GetComponent<Health>();

        if (hth != null)
            hth.TakeDamage(Damage);
    }
}
