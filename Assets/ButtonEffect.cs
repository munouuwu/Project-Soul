using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEffect : MonoBehaviour
{
    [SerializeField] TestButton buttton;
    [SerializeField] List<GameObject> objEnabledAtTrue;
    [SerializeField] List<GameObject> objDisableAtTrue;

    private void Start()
    {
        buttton.buttonPressEvent -= OnButtonTrigger;
        buttton.buttonPressEvent += OnButtonTrigger;
    }

    private void OnButtonTrigger(bool state)
    {
        foreach(GameObject obj in objEnabledAtTrue)
        {
            if (obj != null)
                obj.SetActive(state);
        }
        foreach (GameObject obj in objDisableAtTrue)
        {
            if(obj != null)
            obj.SetActive(!state);
        }
    }
}
