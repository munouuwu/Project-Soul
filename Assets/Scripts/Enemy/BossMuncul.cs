using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMuncul : MonoBehaviour
{
    [SerializeField] List<GameObject> objToSpawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject obj in objToSpawn)
            {
                obj.SetActive(true);
            }
        }
    }


}
