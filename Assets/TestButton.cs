using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum UnlockButtonWith
{
    knives,
    thorwable
}
public class TestButton : MonoBehaviour
{
    public Action<bool> buttonPressEvent;

    public bool buttonState;

    public bool singleToggle;

    public UnlockButtonWith unlockWith;

    [Header("Animation/FeedBack")]
    public Transform offState;
    public Transform onState;

    private void Start()
    {
        //buttonPressEvent = OnButtonTrigger;
        UpdateButtonDisplay();
    }

    private void OnEnable()
    {
        UpdateButtonDisplay();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (singleToggle && buttonState) return;
        if (unlockWith == UnlockButtonWith.thorwable)
        {
            if (collision.transform.GetComponent<ThrowableObject>() != null)
            {
                
                buttonState = !buttonState;
                ButtonTrigger(buttonState);
                UpdateButtonDisplay();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (singleToggle && buttonState) return;
        if (unlockWith == UnlockButtonWith.knives)
        {
            if (collision.transform.GetComponent<KnifeLocal>() != null)
            {
                buttonState = !buttonState;
                ButtonTrigger(buttonState);
                UpdateButtonDisplay();
            }
        }
    }

    private void UpdateButtonDisplay()
    {
        if (onState == null || offState == null) return;
        onState.gameObject.SetActive(buttonState);
        offState.gameObject.SetActive(!buttonState);
    }
        

/*    private void OnButtonTrigger(bool state)
    {
        Debug.Log("button triggered " + buttonState);
    }*/

    private void ButtonTrigger(bool on)
    {
        if(buttonPressEvent != null)
            buttonPressEvent(on);
    }
}
