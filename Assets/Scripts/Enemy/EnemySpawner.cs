using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public float initialSpawnDelay = 0.5f;
    public Vector2 spawnDelayTime = new Vector2(0.5f, 2f);
    float spawnDelay;

    public GameObject[] enemyEntity;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoint == null) spawnPoint = transform;
        spawnDelay = initialSpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnDelay > 0)
        {
            spawnDelay -= 1f * Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            spawnDelay = Random.Range(spawnDelayTime.x, spawnDelayTime.y);
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyEntity[Random.Range(0, enemyEntity.Length)], spawnPoint.position, spawnPoint.rotation);
    }
}
