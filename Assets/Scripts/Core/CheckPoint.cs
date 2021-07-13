using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool cpPassed = false;

    //public Transform playerResetTransform;

    [System.Serializable]
    public class CheckedObjects
    {
        public Vector2 originPos;
        /*public bool isPreSpawned = true;*/
        /*public bool isNoPrefab;*/
        public GameObject preSpawned;
        public GameObject goPrefab;


        public void InStart()
        {
            originPos = preSpawned.transform.position;

            if (goPrefab == null)
            {
                goPrefab = Instantiate(preSpawned);
                goPrefab.SetActive(false);
            }
        }

        public void ResetObject()
        {
            if(preSpawned != null)
            {
                Destroy(preSpawned);
                preSpawned = null;
            }

            preSpawned = Instantiate(goPrefab, originPos, Quaternion.identity);
            preSpawned.SetActive(true);

        }
    }

    public CheckedObjects[] cobjs;


    // Start is called before the first frame update
    void Start()
    {
        foreach(CheckedObjects cobj in cobjs)
        {
            cobj.InStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Checkpoint " + gameObject.name + " was passed");
            cpPassed = true;
        }
    }

    public void CheckPointReset(GameObject dePlayer)
    {
        dePlayer.transform.position = this.transform.position;

        foreach(CheckedObjects cobj in cobjs)
        {
            cobj.ResetObject();
        }
    }
}
