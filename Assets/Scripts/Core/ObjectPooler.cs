using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<PoolObjectClass> poolClassArray;

    void Start()
    {
        StartCoroutine(InitializePool());
        
    }

    private IEnumerator InitializePool()
    {
        foreach (PoolObjectClass poolClass in poolClassArray)
        {
            for (int i = 0; i < poolClass.numberObjectToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(poolClass.objectToPool);
                obj.SetActive(false);
                poolClass.objectPool.Add(obj);
                
            }
            yield return null;
        }
    }

    

    public GameObject GetPooledObject(string tagSearch)
    {
        foreach (PoolObjectClass poolClass in poolClassArray)
        {
            if (poolClass.tagName == tagSearch)
            {
                for (int i = 0; i < poolClass.objectPool.Count; i++)
                {
                    if (!poolClass.objectPool[i].activeInHierarchy)
                        return poolClass.objectPool[i];
                }
            }
        }
        return null;
    }
}

[System.Serializable]
public class PoolObjectClass
{
    public string tagName;

    public int numberObjectToPool;
    public GameObject objectToPool;

    public List<GameObject> objectPool;
}
