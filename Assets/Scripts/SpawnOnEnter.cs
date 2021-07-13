using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnEnter : MonoBehaviour
{
    public GameObject[] enemies; //dragged 6 GameObjects into Inspector
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        //activate enemies
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }

        Destroy(gameObject);
    }
}
