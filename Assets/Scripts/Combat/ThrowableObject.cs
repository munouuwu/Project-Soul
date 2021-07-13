using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public bool isPullAble = true;
    public bool isPulled = false;

    public float highestImpulse = 0f;
    public float recentImpulse = 0f;
    Rigidbody2D tobRig;

    public float oppObjectvelMag = 0f;
    public float oppObjectMass = 0f;

    public float impulseThreshold = 20f;
    public float impulseDamageMultiplier = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        tobRig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.transform.name);

        if(collision.rigidbody != null)
        {
            oppObjectvelMag = collision.rigidbody.velocity.magnitude;
            oppObjectMass = collision.rigidbody.mass;
        }

        recentImpulse = (tobRig.velocity.magnitude * tobRig.mass) + (oppObjectvelMag * oppObjectMass);

        if (recentImpulse > highestImpulse) highestImpulse = recentImpulse;

        if(GetComponent<Health>() != null)
        {
            if(recentImpulse > impulseThreshold)
            {
                GetComponent<Health>().TakeDamage(recentImpulse * impulseDamageMultiplier);
            }
        }

        
        oppObjectMass = 0f;
        oppObjectvelMag = 0f;
    }
}
