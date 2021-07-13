using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    IEnumerator Interact(Transform playerObj);
    //void InstantInteract(Transform playerObj);
    string GetInteractableName();

}
