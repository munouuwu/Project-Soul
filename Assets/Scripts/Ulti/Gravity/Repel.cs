using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Repel : MonoBehaviour,IUltimate
{

    public GameObject RepelObj;

    private PlayerMeleeatk meleeattackk;

    

    void Awake()
    {
        meleeattackk = gameObject.GetComponent<PlayerMeleeatk>();
        
    }

    public void Surround()
    {

        /*if (meleeattackk.UltiCounter == 5)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Q pressed");
                Instantiate(RepelObj, transform.position, Quaternion.identity);
                meleeattackk.UltiCounter = 0;
            }
        }*/

    }

    public void InvokeUltimate(Vector2 mousePos)
    {
        Instantiate(RepelObj, mousePos, Quaternion.identity);
        
    }
}
