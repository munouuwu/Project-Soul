using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatWitchControl : MonoBehaviour
{
    public KnifeThrow kt;
    public GravityPowerSystem gp;

    public bool stdCombat = true;

    public Text modeText;

    public KeyCode toggleKey;

    // Start is called before the first frame update
    void Start()
    {
        if (stdCombat)
        {
            ActivateKnife();
        }
        else
        {
            ActivateGravity();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (stdCombat)
            {
                ActivateGravity();
            }
            else
            {
                ActivateKnife();
            }
        }

        
    }

    void ActivateKnife()
    {
        kt.active = true;
        gp.active = false;
        modeText.text = "COMBAT MODE: WITCH-STD";
        stdCombat = true;
    }

    void ActivateGravity()
    {
        kt.active = false;
        gp.active = true;
        modeText.text = "COMBAT MODE: WITCH-DYNMC";
        stdCombat = false;
    }
}
