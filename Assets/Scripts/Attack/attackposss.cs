using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackposss : MonoBehaviour
{
    public float offset;

    // Update is called once per frame
    void Update()
    {
        Position();
    }

    public void Position()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }
}
