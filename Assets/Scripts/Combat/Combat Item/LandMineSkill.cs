using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineSkill : MonoBehaviour, ICombatItems
{
    [SerializeField]
    CombatItemType itemType;
    [SerializeField]
    private float cooldownTime =2f;

    [SerializeField]
    private int usesPerCooldown = 3;
    private int used;

    [Header("Landmine")]
    public Transform landMinePool;
    /*[SerializeField]
    private GameObject landMinePrefab;*/
    [SerializeField]
    Transform playerTransform;

    public GameObject prefab;


    public bool CanUse()
    {
        return true;
    }

    public float GetCoolDownTime()
    {
        return cooldownTime;
    }

    public void UseItem()
    {
        Debug.Log("Deploy LandMine");
        GameObject obj = GetNonActiveObject();
        if (obj == null) return;
        obj.SetActive(true);
        obj.transform.position = playerTransform.position;

        //GameObject obj = Instantiate(prefab, playerTransform.position, Quaternion.identity);
        
    }

    public void ResetUse()
    {
        
    }

    public GameObject GetNonActiveObject()
    {
        foreach (Transform objTransform in landMinePool)
        {
            if (!objTransform.gameObject.activeInHierarchy)
                return objTransform.gameObject;
        }

        return null;
    }
}
