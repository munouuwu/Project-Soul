using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbPlatform : MonoBehaviour,IInteractable
{
    
    [SerializeField] Transform destination;
    [SerializeField] float climbSpeed = 0.5f;
    [SerializeField] string objName;
    // Start is called before the first frame update
    public IEnumerator Interact(Transform playerObj)
    {
        /*Debug.Log("jalan");*/

        CharacterMovement movement =  playerObj.GetComponent<CharacterMovement>();
        Rigidbody2D rb = playerObj.GetComponent<Rigidbody2D>();
        movement.enabled = false;

        while( Vector2.Distance(destination.position, playerObj.position) > 0.1f)
        {
/*            Debug.Log("jalan loop");*/
            playerObj.position = Vector2.MoveTowards(playerObj.position, destination.position, climbSpeed * Time.deltaTime);
            //rb.MovePosition(Vector2.Lerp(playerObj.position, destination.position ,  climbSpeed * Time.deltaTime));
            yield return null;
        }
        movement.enabled = true;
        Debug.Log("done");
        yield return null;
    }

    public string GetInteractableName()
    {
        return "Climb " + objName;
    }
}
