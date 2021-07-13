using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonedHand : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Health>().TakeDamage(20);
        }
    }

    public void AE_OnHandEnd()
    {
        Destroy(gameObject);
    }
}
