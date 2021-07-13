using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBomb : MonoBehaviour
{
    ThrowableObject throwableObject;
    Animator bombAnimator;

    public bool isFuse;
    public float timeBeforeExplode = 5f;

    public GameObject explosionPrefab;

    public float overrideDamage = 50f;

    // Start is called before the first frame update
    void Start()
    {
        throwableObject = GetComponent<ThrowableObject>();
        bombAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (throwableObject.isPulled == true)
        {
            bombAnimator.SetBool("Pulled", true);
            isFuse = true;
        }

        if (isFuse)
        {
            if(timeBeforeExplode > 0)
            {
                timeBeforeExplode -= 1f*Time.deltaTime;
            }
            else
            {
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("Explosion");
                }
                GameObject leadakan = Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
                //Debug.Log("ADA LEDAKAN!!!!");
                leadakan.GetComponent<BasicExplosion>().damage = overrideDamage;
                Destroy(gameObject);
            }
        }
    }
}
