using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    public bool startByTrigger;

    [Header("EnemySpawn")]
    [SerializeField] List<GameObject> enemiesToSpawn;
    GameObject spawnAnimation;
    
    public void StartEncounter()
    {
        SpawnEnemies();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!startByTrigger || collision.transform.tag != "Player") return;

        
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach (GameObject obj in enemiesToSpawn)
        {
            if (spawnAnimation != null) Instantiate(spawnAnimation, obj.transform.position, Quaternion.identity);
            obj.SetActive(true);
        }
    }
}
