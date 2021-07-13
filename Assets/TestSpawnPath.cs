using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnPath : MonoBehaviour,IInteractable
{
    [SerializeField] bool activateOnEnable;
    [SerializeField] List<GameObject> objectsToSpawn;
    [SerializeField] List<GameObject> objectsToDespawn;

    private void OnEnable()
    {
        if(activateOnEnable)
        {
            StartCoroutine(Interact(transform));
        }
    }

    public string GetInteractableName()
    {
        return "TestSpawnPath";
    }

    public void InstantInteract(Transform playerObj)
    {
        return;
    }

    public IEnumerator Interact(Transform playerObj)
    {

       
       foreach(GameObject obj in objectsToSpawn)
       {
            obj.SetActive(true);
            yield return null;
        }
       
        foreach (GameObject obj in objectsToDespawn)
        {
            obj.SetActive(false);
            yield return null;

        }

    }
}
