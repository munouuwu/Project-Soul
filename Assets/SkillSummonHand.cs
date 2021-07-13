using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSummonHand : MonoBehaviour
{
    [SerializeField] GameObject objToSpawn;
    [SerializeField] int noOfSummon;
    [SerializeField] float delayPerSummon = 0.1f;
    Transform playerObj;
    private void OnEnable()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SummonHand());
    }

    public IEnumerator SummonHand()
    {
        for(int i = 0; i < noOfSummon; i++)
        {
            Instantiate(objToSpawn, playerObj.position + new Vector3(0,2,0), Quaternion.identity);
            yield return new WaitForSeconds(delayPerSummon);
        }
        gameObject.SetActive(false);
    }
}
