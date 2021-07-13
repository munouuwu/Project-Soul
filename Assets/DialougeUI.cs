using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform rt;

    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private Vector2 targetPivot;

    private Vector2 originalPivot;

    [SerializeField]
    private Text textUI;

    private bool isOpen;
    public bool IsOpen => isOpen;

    void Start()
    {
        originalPivot = rt.pivot;
    }

    public IEnumerator DisplayText(string text)
    {
        ClearText();

        foreach(char a in text)
        {
            textUI.text = textUI.text + a;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.0f);
    }

    /*// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if(!isOpen)
            {
                StartCoroutine(OpenDialouge());
                isOpen = true;
            }else
            {
                StartCoroutine(CloseDialouge());
                isOpen = false;
            }
            
        }
    }*/

    public IEnumerator CloseDialouge()
    {

        Vector2 pivot = rt.pivot;

        while (pivot != originalPivot)
        {
            pivot = Vector2.MoveTowards(pivot, originalPivot, speed * Time.deltaTime);
            rt.pivot = pivot;
            yield return null;
        }

        ClearText();

        isOpen = false;
    }

    private void ClearText()
    {
        textUI.text = "";
    }

    public IEnumerator OpenDialouge()
    {
        isOpen = true;

        Vector2 pivot = originalPivot; 

        while(pivot != targetPivot)
        {
            pivot = Vector2.MoveTowards(pivot, targetPivot, speed * Time.deltaTime);
            rt.pivot = pivot;
            yield return null;
        }
        

    }
}
