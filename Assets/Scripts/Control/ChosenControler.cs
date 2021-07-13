using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum PathFindingTag
{
    Player,
    Enemy,
    NPC
}

public class ChosenControler : MonoBehaviour
{
    private CharacterMovement movement;
    private Vector2 direction;

    private ShieldSystem shield;
    private PlayerMeleeatk meleeAtack;
    private UltimateSystem ultimate;
    //private LightningStrike lightningUlt;
    private Repel repelUlt;

    InteractSystem interactSystem;
    public PathFindingTag tes;

    


    
    void Start()
    {
        movement = GetComponent<CharacterMovement>();
        shield = GetComponent<ShieldSystem>();
        meleeAtack = GetComponent<PlayerMeleeatk>();
        ultimate = GetComponent<UltimateSystem>();
        interactSystem = GetComponent<InteractSystem>();
        //repelUlt = GetComponent<Repel>();

        transform.tag = tes.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = dir.normalized;

        if(Input.GetMouseButtonDown(0))
        {
            meleeAtack.Combos_();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            ultimate.UseUltimate(Input.mousePosition, 0);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            interactSystem.Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ultimate.UseUltimate(transform.position, 1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            shield.ShieldsUp();
            
        }
        else
        {
            shield.ShieldsDown();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }


        if(shield.isDoingSkill())
        {
            direction = Vector2.zero;
        }

        movement.InputKeyboard(direction);

       /* GameExit();*/


    }

   /* private void GameExit()
    {
        if (Input.GetKey("escape"))
        {
            Debug.Log("game ended");
            Application.Quit();
        }
    }*/


}
