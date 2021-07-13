using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeForPuzzle : MonoBehaviour
{
    public float CameraSize;
    public Transform CameraPosition;
    CamFollow camFollow;
    Camera cam;

    private void Start()
    {
        camFollow = Camera.main.GetComponent<CamFollow>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            camFollow.SetCamera(CameraPosition, CameraSize);
        }
    }


}
