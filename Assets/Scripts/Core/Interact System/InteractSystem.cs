using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    public ChangeHeroTrigger trig;
    public IInteractable interactable;
    public Transform interactableObj;
    private IInteractable onInteract;
    public bool interacting;
    string currentInteraction = "";

    public void Interact ()
    {
        
        if (!interacting)
        {

            //if (interactableObj == null) return;
            if (interactable == null) return;
            interacting = true;
            interactable = interactableObj.GetComponent<IInteractable>();

            onInteract = interactable;
            StartCoroutine(InteractProcess());
        }


    }

    private void OnEnable()
    {
        interacting = false;
        RemoveInteractable();
    }

    private IEnumerator InteractProcess()
    {

        yield return onInteract.Interact(transform);
        interacting = false;


    }

    public void RemoveInteractable()
    {
        interactable = null;
        interactableObj = null;
    }

    
}
