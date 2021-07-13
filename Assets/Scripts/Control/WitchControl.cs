using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WitchControl : MonoBehaviour
{
    GravityPowerSystem gravityPower;
    KnifeThrow knifeThrow;
    CharacterMovement movement;
    CharacterDash dash;
    InteractSystem interactSystem;

    // Additional Control
    HeadAimDirection playerAim;
    private Vector2 keyboardInput;

    //Switch Knife Throw / Gravity Push Pull
    private bool changeMode;

    public Color onColor;
    public Color offColor;
    public Image knifeUI;
    public Image gravityUI;

    Animator playerAnimator;
    void Start()
    {
        movement = GetComponent<CharacterMovement>();
        dash = GetComponent<CharacterDash>();
        knifeThrow = GetComponent<KnifeThrow>();
        gravityPower = GetComponent<GravityPowerSystem>();
        playerAim = GetComponent<HeadAimDirection>();
        interactSystem = GetComponent<InteractSystem>();
        playerAnimator = GetComponent<Animator>();

        //knifeUI.color = onColor;
        //gravityUI.color = offColor;
    }

    // Update is called once per frame
    void Update()
    {
        var dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        keyboardInput = dir.normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dash.dashCharge >= dash.dashChargeUsage)
            {
                dash.DoSkill(keyboardInput);
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            /*if (!changeMode)
            {
                knifeThrow.ThrowKnife(playerAim.GetMousePoint);
                
            }
            else
            {
                gravityPower.PullPush(playerAim.GetMousePoint);
                
            }*/
           
            if(gravityPower.coAmount > 0)
            {
                gravityPower.PushObject();
            }
            else
            {
                playerAnimator.SetTrigger("Attacking");
                knifeThrow.ThrowKnife(playerAim.GetMousePoint);
            }
                
        }

        if (Input.GetMouseButtonDown(1))
        {
            gravityPower.PullObject(playerAim.GetMousePoint);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            interactSystem.Interact();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            changeMode = !changeMode;
            if (!changeMode)
            {
                //knifeUI.color = onColor;
                //gravityUI.color = offColor;
                gravityPower.DropObject();
            }
            else
            {
                //knifeUI.color = offColor;
                //gravityUI.color = onColor;
            }
                
        }

        movement.InputKeyboard(keyboardInput);
    }

    private void FixedUpdate()
    {
        WitchAnimationAttack();
    }

    private void WitchAnimationAttack()
    {
        //get mousePosition
        Vector3 posisiMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //jarak mouse ke player
        Vector3 jarakMouseKePlayer = posisiMouse - transform.position;

        playerAnimator.SetFloat("Attack_X", jarakMouseKePlayer.x);
        playerAnimator.SetFloat("Attack_Y", jarakMouseKePlayer.y);

    }

}
