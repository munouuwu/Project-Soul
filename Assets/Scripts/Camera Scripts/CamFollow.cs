using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform targetFollow;
    public float followSpeed = 5f;
    private Transform defaultTargetFollow;
    private float defaultCameraSize;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        defaultTargetFollow = GameObject.FindGameObjectWithTag("Player").transform;
        targetFollow = defaultTargetFollow;
        cam = GetComponent<Camera>();
        defaultCameraSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, new Vector3(targetFollow.position.x,targetFollow.position.y,transform.position.z), followSpeed*Time.deltaTime);
    }

    public void SetToDefault()
    {
        targetFollow = defaultTargetFollow;
        cam.orthographicSize = defaultCameraSize;
    }

    public void SetCamera(Transform target, float camSize)
    {
        targetFollow = target;
        cam.orthographicSize = camSize;
    }
}
