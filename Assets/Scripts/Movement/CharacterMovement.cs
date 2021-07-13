using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Patch Notes :
///  - DashingCor()  is the new dash system
///  already fixes the glitching issues causes by trigger collider
///  that used StopDash() function
///  
///  - Next Fix is to change dash form rb.velocity to rb.MovePosition(vec2)
/// </summary>
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Walking")]
    [SerializeField]
    private float moveSpeed = 10f;
    private Vector2 direction = Vector2.zero;

    [Header("Dashing")]
    [SerializeField]
    private float dashSpeed = 12;
    [SerializeField]
    private float dashDistance = 5;

    private bool isDashing = false;
    public bool IsDashing => isDashing;
    private Vector2 dashTarget;

   

    // Animator
    public Animator playerAnimatorMove;
    //private CharacterController moveanim;

    //New Dash Implementation
   

    

    

    public LayerMask jurangLayer;
    public LayerMask playerLayer;

    

    public bool cannotMove;


    [Header("Dash Param")]
    [SerializeField] Transform playerBase;
    [SerializeField] float testRad;
    private float dashTimer = 0f;
    public float dashDuration = 3f;

    private Vector2 dashDirection;
    private Vector2 dashOrigin;
    public float dashReturnSpeed = 12;
    private bool redooDash;
    //private bool movingPlatform;

    [Header("On moving Platform")]
    [SerializeField] float mod_MovingPlatform = 1;
    private float modMP = 1;
    //experitmental
    bool onMoving;
    MovingPlatformTest movingPlatform;
    public LayerMask platformLayer;

    public DashAfterImage dai;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //moveanim = GetComponent<PlayerController>();

    }

    private void Update()
    {
        
    }

    private void DashReturn()
    {
        redooDash = true;
        isDashing = true;
    }

    private void FixedUpdate()
    {
        onMoving = OnPlatform();
        if (cannotMove) return;
        if(!isDashing)
        {
            if(!onMoving)
            {
                var speed = moveSpeed * modMP;
                rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

                
            }
            else if(movingPlatform != null)
            {
                var speed = moveSpeed;
                Vector2 vec = new Vector2(direction.x * speed, direction.y * speed) + movingPlatform.GetRb().velocity;

                
            }

            PlayerAnimationMove();

        }
        /*else if(!redooDash)
        {
            Dashing();
            

        } 
        else
        {
            ReturnToOrigin();
        }*/
        
    }

    public void InputKeyboard(Vector2 dir)
    {
        
        direction = dir;
        
    }

    public void Dash(Vector2 dir)
    {

        dashDirection = dir;
        dashOrigin = transform.position;
        dashTarget = new Vector2(transform.position.x, transform.position.y) + dashDistance * dir;
        isDashing = true;
        SetCollision(true);
        StartCoroutine(DashingCor());
    }

    private void SetCollision(bool ignore)
    {
        
        //Physics2D.GetIgnoreLayerCollision()
        
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Jurang"), ignore);
    }

    public bool OnPlatform()
    {
        
        if (playerBase == null) return false;
        Collider2D collider = Physics2D.OverlapCircle(playerBase.position, testRad, platformLayer);
        if (collider != null)
        {
            
            movingPlatform = GetComponent<MovingPlatformTest>();
            return true;
        }
        else
        {
            movingPlatform = null;
            return false;
        }

        
    }

    private IEnumerator DashingCor()
    {
        Vector2 or = transform.position;
        while(dashTimer < dashDuration)
        {
            //Debug.Log("dshh");
            //dai.SpawnAfterImage(transform.position);
            dashTimer += Time.deltaTime;
            rb.velocity = dashDirection * dashSpeed;
            yield return null;
        }

        Vector2 des = transform.position;
        float dis = Vector2.Distance(or, des);


        bool gnd = isGrounded();

        if(!gnd)
        {
            while (Vector2.Distance(transform.position, dashOrigin) > 0.1f)
            {
                rb.MovePosition(Vector2.Lerp(transform.position, dashOrigin, dashReturnSpeed / 2));
                yield return null;
                
            }

            StopDash();

        }
        else
        {
            StopDash();
        }

        

        
        yield return null;
    }

    private void Dashing()
    {
        /*if (Vector2.Distance(transform.position, dashTarget) < 0.3f)
        {
            StopDash();
        }       
        rb.MovePosition(Vector2.Lerp(transform.position,dashTarget,dashSpeed*Time.deltaTime));*/

        if (dashTimer > dashDuration)
        {
            if (isGrounded())
                StopDash();
            else
                DashReturn();

            return;
        }

        dashTimer += Time.deltaTime;
        rb.velocity = dashDirection * dashSpeed;
        WitchDashAnim();
        Debug.Log("ngedash");
    }

    public void ReturnToOrigin()
    {
        if (Vector2.Distance(transform.position, dashOrigin) < 0.1f)
        {
            StopDash();
           
        }

        Debug.Log("balik");
        rb.MovePosition(Vector2.Lerp(transform.position, dashOrigin, dashReturnSpeed * Time.deltaTime));
    }

    


    private void StopDash()
    {
        redooDash = false;
        dashTimer = 0;
        isDashing = false;

        SetCollision(false);
        /*bool grounded = isGrounded();

        if(!grounded)
        {
            DashReturn();
        }*/

        /*if(isGrounded())
        {
            redooDash = false;
            dashTimer = 0;
            isDashing = false;
        }
        else
        {
            DashReturn();
        }*/

    }

    private void OnDrawGizmosSelected()
    {
        if (playerBase == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerBase.position, testRad);
    }

    private bool isGrounded()
    {
        if (playerBase == null) return false;
        Collider2D[] collider = Physics2D.OverlapCircleAll(playerBase.position, testRad,jurangLayer);
        if(collider.Length > 0)
        {
            Debug.Log(collider[0].name);
            return false; 
        }

        return true;
        
    }

   /* public void OnMovingPlatform(bool on)
    {
        if (on)
            modMP = mod_MovingPlatform;
        else
            modMP = 1;
    }*/

   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        
        *//*StopDash();*//*
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        *//*if (isDashing)
            StopDash();*//*

    }*/
    

    private void PlayerAnimationMove()
    {
        playerAnimatorMove.SetFloat("MoveX", rb.velocity.x);
        playerAnimatorMove.SetFloat("MoveY", rb.velocity.y);


        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            playerAnimatorMove.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            playerAnimatorMove.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }

    private void WitchDashAnim()
    {
        playerAnimatorMove.SetTrigger("Dash");

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            playerAnimatorMove.SetFloat("DashX", Input.GetAxisRaw("Horizontal"));
            playerAnimatorMove.SetFloat("DashY", Input.GetAxisRaw("Vertical"));
        }
    }

}
