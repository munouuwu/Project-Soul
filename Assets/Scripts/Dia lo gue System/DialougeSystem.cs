using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeSystem : MonoBehaviour
{
    [SerializeField]
    private DialougeUI dialougeUI;

    [SerializeField]
    List<string> dialougeList;


    public string contohText;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            //Debug.Log(dialougeList);

            if(!dialougeUI.IsOpen)
                StartCoroutine(StartDialouge());
        }
    }

    public IEnumerator StartDialouge()
    {
        yield return dialougeUI.OpenDialouge();

        foreach(string text in dialougeList)
        {
            yield return dialougeUI.DisplayText(text);
        }


        yield return dialougeUI.CloseDialouge();
    }
    
}
