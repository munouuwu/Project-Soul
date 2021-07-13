using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public CheckPoint[] cps;
    public GameObject playerObject;

    Health playerHealth;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerObject.GetComponent<Health>();

        playerHealth.OnDeathEvent -= PlayerResetLastCP;
        playerHealth.OnDeathEvent += PlayerResetLastCP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayerResetLastCP()
    {
        for(int i = cps.Length-1; i >= 0; i--)
        {
            if (cps[i].cpPassed)
            {
                cps[i].CheckPointReset(playerObject);

                playerHealth.Ressurect(playerHealth.MaxHP);
                Debug.Log("PLAYER RESET TO LAST CHECK POINT OF = CP(" + i + ")");
                break;
            }
        }
    }
}
