using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public enum DestinationTag
    {
        A,
        B,
        C,
        D,
        E
    }

    [SerializeField] int sceneToLoad = -1;

    [SerializeField] public DestinationTag destination;
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {

        if(sceneToLoad < 0)
        {
            Debug.Log("No Scene To Load");
            yield break;
        }

        DontDestroyOnLoad(gameObject);

        Portal spawnPortal = GetOtherPortal();

        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        //After Load Scene
        UpdatePlayerPosition(spawnPortal);

        yield return null;
    }

    private void UpdatePlayerPosition(Portal spawnPortal)
    {
        GameObject obj = GameObject.FindWithTag("Player");
        obj.transform.position = spawnPortal.transform.position;
    }

    private Portal GetOtherPortal()
    {
        foreach(Portal portal in FindObjectsOfType<Portal>())
        {
            if (portal == this) continue;
            if (portal.destination == destination)
                return portal;
        }

        return null;
    }
}
