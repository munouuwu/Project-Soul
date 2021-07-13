using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Transform obstacle;
    public float ispeed;
    public float speed;
    public float acc;
    public Vector2 stopTime = new Vector2(2f,0f);
    public Transform[] wp;
    public int wpIndex;
    public bool move;
    
    

    // Start is called before the first frame update
    void Start()
    {
        speed = ispeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(obstacle.position == wp[wpIndex].position)
        {
            if(stopTime.y < stopTime.x)
            {
                stopTime.y += 1f * Time.deltaTime;
            }
            else
            {
                wpIndex++;
                if(wpIndex >= wp.Length)
                {
                    wpIndex = 0;
                }
                speed = ispeed;
                stopTime.y = 0f;
            }
        }
        else
        {
            obstacle.position = Vector3.MoveTowards(obstacle.position, wp[wpIndex].position, speed * Time.deltaTime);
            speed += acc * Time.deltaTime;
        }

    }
}
