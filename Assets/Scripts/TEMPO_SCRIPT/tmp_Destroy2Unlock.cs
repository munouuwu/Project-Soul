using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmp_Destroy2Unlock : MonoBehaviour
{
    public GameObject[] theOrbs;

    

    public int orbAmount2Unlock;

    public GameObject theGate;

    public bool isOpened;
    public float openingTime = 1f;
    Animator gateTor;
    // Start is called before the first frame update
    void Start()
    {
        orbAmount2Unlock = theOrbs.Length;

        foreach(GameObject orb in theOrbs)
        {
            orb.GetComponent<Health>().OnDeathEvent -= OneOrbDestroyed;
            orb.GetComponent<Health>().OnDeathEvent += OneOrbDestroyed;
        }

        gateTor = theGate.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpened)
        {
            gateTor.SetBool("isOpened", true);

            if(openingTime > 0)
            {
                openingTime -= 1f * Time.deltaTime;
            }
            else
            {
                theGate.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }


    public void OneOrbDestroyed()
    {
        orbAmount2Unlock--;

        if(orbAmount2Unlock == 0)
        {
            isOpened = true;
        }
    }
}
