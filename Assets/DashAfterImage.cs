using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAfterImage : MonoBehaviour
{
    public ObjectPooler pooler;

    [SerializeField]bool canSpawn;

    float timer = Mathf.Infinity;
    private void Update()
    {
        if(timer > 1f)
        {
            canSpawn = true;
        } 
        else
        {
            timer += Time.time;
        }
    }
    public void SpawnAfterImage(Vector2 pos)
    {
        if (!canSpawn) return;
        if (pooler == null) return;
        GameObject obj = pooler.GetPooledObject("DAI");
        if (obj == null) return;
        
        obj.transform.position = pos;
        obj.SetActive(true);

        timer = 0;
        canSpawn = false;
    }
}
