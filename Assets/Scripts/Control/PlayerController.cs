using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement movement;
    private Vector2 direction;

    private ShieldSysten uniqueSkill;

    [SerializeField]
    private KeyCode dashKey;
    void Start()
    {
        movement = GetComponent<CharacterMovement>();
        uniqueSkill = GetComponent<ShieldSysten>();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = dir.normalized;



        SkillControl();
        
        movement.InputKeyboard(direction);

        GameExit();
    }

    private void SkillControl()
    {
        if(Input.GetKeyDown(dashKey))
        {
            uniqueSkill.DoSkill(direction);
        }
    }

    private void GameExit()
    {
        if (Input.GetKey("escape"))
        {
            Debug.Log("game ended");
            Application.Quit();
        }
    }
}
