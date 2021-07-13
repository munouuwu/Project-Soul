using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPzl : MonoBehaviour
{
    public KeyCode interactKey;
    public bool playerInTrigger;

    public Animator leverTor;
    public GameObject objekToActive;
    public GameObject objekToNonActive;
    public bool toggleMode = true;
    public bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        OnLeverNonActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger)
        {
            if (toggleMode)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    if (isOn)
                    {
                        OnLeverNonActive();
                        isOn = false;
                    }
                    else
                    {
                        OnLeverActive();
                        isOn = true;
                    }
                }
            }
            else
            {
                if (Input.GetKey(interactKey))
                {
                    isOn = true;
                    OnLeverActive();
                }
                else
                {
                    isOn = false;
                    OnLeverNonActive();
                }
            }
        }
        else
        {
            if (!toggleMode)
            {
                if (isOn)
                {
                    isOn = false;
                    OnLeverNonActive();
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerInTrigger = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerInTrigger = false;
    }

    public void OnLeverActive()
    {
        if (leverTor != null) leverTor.SetBool("isOn", true);
        if (objekToActive != null) objekToActive.SetActive(true);
        if (objekToNonActive != null) objekToNonActive.SetActive(false);
    }

    public void OnLeverNonActive()
    {
        if (leverTor != null) leverTor.SetBool("isOn", false);
        if (objekToActive != null) objekToActive.SetActive(false);
        if (objekToNonActive != null) objekToNonActive.SetActive(true);
    }
}
