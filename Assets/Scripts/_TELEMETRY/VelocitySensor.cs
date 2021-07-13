using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySensor : MonoBehaviour
{
    public Vector2 rigidVelocity;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidVelocity = GetComponent<Rigidbody2D>().velocity;
    }
}
