using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    [SerializeField] InteractSystem interactSystem;
    
    string interactableName ="";
    void Start()
    {
        if (interactSystem == null)
            interactSystem = transform.parent.GetComponent<InteractSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if(collision.tag != "Player")
        {


            IInteractable interactable = collision.GetComponent<IInteractable>();
            if ( interactable != null)
            {
                /*Debug.Log(collision.name);*/
                //interactableName = interactable.GetInteractableName();
                Debug.Log("Interactable Enter: " + interactable.GetInteractableName());
                interactSystem.interactable = interactable;
                interactSystem.interactableObj = collision.transform;
               

            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactSystem.interactable == null || interactable == null) return;
        if(interactSystem.interactable.GetInteractableName() == interactable.GetInteractableName())
        {
            
            Debug.Log("Interactable Exit : " + collision.name);
            interactSystem.RemoveInteractable();
            
        }

        
    }
}
