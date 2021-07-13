using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSakit : MonoBehaviour
{
    
    public Health playerHealth;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("kena spike");
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(10);
        }
    }
}
