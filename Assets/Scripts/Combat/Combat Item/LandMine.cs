using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField]
    Transform explosionObjPool;
    private bool hasExploded;
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        col.enabled = true;
    }

    private void OnDisable()
    {
        hasExploded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasExploded)
        {
            if(collision.tag == "Enemy")
            {
                hasExploded = true;
                GetComponent<CircleCollider2D>().enabled = false;
                TriggerLandMine();
            }
        }
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("wat");
    }


    private void TriggerLandMine()
    {
        GameObject obj = GetNonActiveObject();
        obj.SetActive(true);
        obj.transform.position = transform.position;
        //Instantiate(explosionPrefab, transform.position,Quaternion.identity);
        gameObject.SetActive(false);
    }

    public GameObject GetNonActiveObject()
    {
        foreach (Transform objTransform in explosionObjPool)
        {
            if (!objTransform.gameObject.activeInHierarchy)
                return objTransform.gameObject;
        }

        return null;
    }
}
