using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject objToDisable;
    public void AE_OnLaserEnd()
    {
        objToDisable.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Health>().TakeDamage(20);
        }
    }
}
