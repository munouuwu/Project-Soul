using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPzl : MonoBehaviour
{

    public Transform homeTarget;
    public GameObject itemTarget;
    public GameObject dummyItem;

    public float distance;
    public float minDistance;

    public bool isCollected = false;

    public Action OnCollectedEvent;

    // Start is called before the first frame update
    void Start()
    {
        dummyItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCollected)
        {
            distance = Vector2.Distance(homeTarget.position, itemTarget.transform.position);

            if (distance <= minDistance)
            {
                isCollected = true;
                itemTarget.SetActive(false);
                dummyItem.SetActive(true);
                OnCollectedEvent.Invoke();
            }
        }
    }
}
