using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform head;
    [SerializeField] LayerMask layer;
    [SerializeField] float rotateSpeed = 1;
    //[SerializeField] GameObject laser;
    float defaultRotateSpeed;
    private void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        /*defaultRotateSpeed = rotateSpeed;
        rotateSpeed = defaultRotateSpeed * rotateSpeed;*/
    }

    private void OnEnable()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        /*defaultRotateSpeed = rotateSpeed;
        rotateSpeed = defaultRotateSpeed * rotateSpeed;*/
    }

    private void Update()
    {
        Vector2 vectorDir = target.position - transform.position;

        float aimDir = Mathf.Atan2(vectorDir.y, vectorDir.x) * Mathf.Rad2Deg - 90f;
        
        
        Quaternion quat = Quaternion.Euler(new Vector3(0, 0, aimDir));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, rotateSpeed * Time.deltaTime);
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.right, 10, layer);
        Color col = Color.white;
        /*if (hit2D != null)
        {
            Debug.Log("wow");
            col = Color.red;
        }
        else
        {
            Debug.Log("wiw");
            col = Color.blue;
        }*/
        Debug.DrawRay(transform.position, transform.right,col);
        
    }

    public void OnAttack()
    {
        //rotateSpeed = defaultRotateSpeed;
    }

    public void OnEndAttack()
    {
        //rotateSpeed = defaultRotateSpeed * 6;
    }


}
