using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningStrike : MonoBehaviour, IUltimate
{
    public GameObject StrikeObject;
    Vector3 StrikePos;
    public int offset = 1;

    public List<GameObject> LightningObject;

    //private PlayerMeleeatk meleeattack;

    //public Slider UltiMeter;

    /*void Awake()
    {
        meleeattack = gameObject.GetComponent<PlayerMeleeatk>();
        
    }*/


    // Update is called once per frame
    void Update()
    {
        //UltiMeter.value = meleeattack.UltiCounter;
        /*UltiMeter.value = meleeattack.UltiCounter;

        StrikePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        StrikePos = new Vector3(StrikePos.x, StrikePos.y + offset);

        if (meleeattack.UltiCounter == 5)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E pressed");
                GameObject obj = GetNonActiveObject();
                if (obj == null) return;
                obj.SetActive(true);
                obj.transform.position = StrikePos;

                meleeattack.UltiCounter = 0;
                UltiMeter.value = 0;
            }

        }*/
    }

    public void Ultimate(Vector2 mousePos)
    {
        /*StrikePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        StrikePos = new Vector3(StrikePos.x, StrikePos.y + offset);

        if (meleeattack.UltiCounter == 5)
        {

            Debug.Log("E pressed");
            GameObject obj = GetNonActiveObject();
            if (obj == null) return;
            obj.SetActive(true);
            obj.transform.position = StrikePos;

            *//*meleeattack.UltiCounter = 0;
            UltiMeter.value = 0;*//*

        }*/
    }

    public GameObject GetNonActiveObject()
    {
        foreach(GameObject obj in LightningObject)
        {
            if(!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        return null;
    }

    public void InvokeUltimate(Vector2 mousePos)
    {
        StrikePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        StrikePos = new Vector3(StrikePos.x, StrikePos.y + offset);

        GameObject obj = GetNonActiveObject();
        if (obj == null) return;
        obj.SetActive(true);
        obj.transform.position = StrikePos;

        
    }
}
