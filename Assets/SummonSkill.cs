using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSkill : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GameObject summonablePrefab;
    [SerializeField] GameObject portalPrefab;

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies ()
    {
        foreach(Transform spawnPoint in spawnPoints)
        {
            if(portalPrefab != null)
                Instantiate(portalPrefab, spawnPoint.position, Quaternion.identity);
            if(summonablePrefab !=null)
                Instantiate(summonablePrefab, spawnPoint.position, Quaternion.identity);
            yield return null;
        }
        gameObject.SetActive(false);

        yield return null;
    }
}
