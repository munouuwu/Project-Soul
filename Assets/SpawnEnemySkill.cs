using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemySkill : MonoBehaviour
{
    [SerializeField] Transform spawnPoints;

    [SerializeField] GameObject enemyPrefab;

    public void SummonEnemy()
    {
        foreach(Transform pos in spawnPoints)
        {
            Instantiate(enemyPrefab, pos.position, Quaternion.identity);
        }

    }
}
