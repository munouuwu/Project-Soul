using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlatform : MonoBehaviour
{
    [SerializeField]Transform follower;
    public Transform following;
    

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MovingPlatform")
        {
            following = collision.transform;
            follower.parent = following;

            var movement = follower.GetComponent<CharacterMovement>();
            if(movement != null)
            {
                //movement.OnMovingPlatform(true); 
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform == following)
        {
            //following = null;
            var movement = follower.GetComponent<CharacterMovement>();
            if (movement != null)
            {
                //movement.OnMovingPlatform(false);
            }
            follower.parent = GameObject.FindGameObjectWithTag("Core").transform;
        }
    }
}
