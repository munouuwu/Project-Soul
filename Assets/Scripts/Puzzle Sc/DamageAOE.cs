using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAOE : MonoBehaviour
{
    List<GameObject> ObjectWithHealth = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!ObjectWithHealth.Contains(collision.gameObject) && collision.GetComponent<Health>() != null)
        {
            ObjectWithHealth.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ObjectWithHealth.Contains(collision.gameObject))
        {
            ObjectWithHealth.Remove(collision.gameObject);
        }
    }

    public void BombExploded(float damage)
    {
        foreach(GameObject objek in ObjectWithHealth)
        {
            objek.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
