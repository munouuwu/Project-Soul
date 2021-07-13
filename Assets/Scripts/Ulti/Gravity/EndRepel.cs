using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRepel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DelayedDestory", 0.6f);
    }

    // Update is called once per frame
    void DelayedDestory()
    {
        Destroy(gameObject);
    }
}
