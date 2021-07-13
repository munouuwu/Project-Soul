using System;
using System.Collections;
using UnityEngine;

public class CharacterDash : MonoBehaviour,ShieldSysten
{
    CharacterMovement movement;
    private bool hasDashed;

    [SerializeField]
    private DashUI dash;

    [SerializeField]
    private float maxDashCharge = 100;

    public float dashChargeUsage = 20;
    public float dashCharge;

    [SerializeField]
    private float regenDelay = 2;
    private float regenDelayTimer = 0;

    [SerializeField]
    private float rechargeMultiplier;

    [Header("For Testing")]
    public bool testWithoutCharge;

    private Animator animatorDash;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        //rb = GetComponent<Rigidbody2D>();
        dashCharge = maxDashCharge;
    }

    void Start()
    {
        animatorDash = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hasDashed)
        {
            regenDelayTimer += Time.deltaTime;
            if (regenDelayTimer >= regenDelay)
            {
                hasDashed = false;
                regenDelayTimer = 0;
            }
        }
        else if (dashCharge < maxDashCharge)
        {
            dashCharge = Mathf.Min(dashCharge + rechargeMultiplier * Time.deltaTime, maxDashCharge);
            UpdateDashUI();

        }
    }

    private void UpdateDashUI()
    {
        //Debug.Log("dC " + dashCharge + " mDC " + maxDashCharge);
        float fraction = dashCharge / maxDashCharge;
        dash.UpdateDashCharge(fraction);
    }

    public void DoSkill(Vector2 directionInput)
    {
        
        if(!movement.IsDashing && directionInput != Vector2.zero)
        {
            if (dashCharge - dashChargeUsage >= 0 || testWithoutCharge )
            {
                DashAnim();
                //Debug.Log("dashing");
                movement.Dash(directionInput);
                dashCharge = Mathf.Max(0, dashCharge - dashChargeUsage);
                UpdateDashUI();
                regenDelayTimer = 0;
                hasDashed = true;
            }
            
        }
        
            
    }

    public bool isDoingSkill()
    {
        return movement.IsDashing;
    }


    public void DashAnim()
    {
        animatorDash.SetTrigger("Dash");

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play("Dash");
            }
            animatorDash.SetFloat("DashX", Input.GetAxisRaw("Horizontal"));
            animatorDash.SetFloat("DashY", Input.GetAxisRaw("Vertical"));
        }
    }



}