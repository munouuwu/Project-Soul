using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotateSpeed;

    private void Start()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    private void OnEnable()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    void Update()
    {
        Vector2 vectorDir = target.position - transform.position;

        float aimDir = Mathf.Atan2(vectorDir.y, vectorDir.x) * Mathf.Rad2Deg - 90f;


        Quaternion quat = Quaternion.Euler(new Vector3(0, 0, aimDir));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, rotateSpeed * Time.deltaTime);
        
    }
}
