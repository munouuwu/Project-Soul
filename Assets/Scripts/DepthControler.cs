using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthControler : MonoBehaviour
{
    
    private SpriteRenderer sr;
    public bool dynamicDepth;
    public float depthOffset;
    public float posY;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = (int)( -1 * (transform.position.y+depthOffset) );
    }

    // Update is called once per frame
    void Update()
    {
        if (!dynamicDepth) return;
        posY = (-1 * transform.position.y + depthOffset);
        sr.sortingOrder = (int)(-1 * transform.position.y + depthOffset);
    }
}
