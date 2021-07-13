using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMousePos : MonoBehaviour
{
    public bool follow;
    SpriteRenderer sRenderer;
    /*float previousX;*/
    // Update is called once per frame

    private void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
       
    }

    void Update()
    {
        if (follow)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(pos.x, pos.y, 0);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if(hit)
            {
                if (hit.transform.GetComponent<ThrowableObject>() != null)
                {
                    sRenderer.color = Color.red;
                }
            }
            else
            {
                sRenderer.color = Color.black;
            }
            
            
        }

        
        /*float dir = transform.position.x - previousX;
        previousX = transform.position.x;*/

    }
}
