using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerEntity;
    private PlayerEntity entity;

    [Range(1, 10)]
    [SerializeField] private float smoothFactor;

    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        entity = playerEntity.GetComponent<PlayerEntity>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPos = playerEntity.transform.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, playerPos, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPos;
    }
}
