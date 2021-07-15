using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerManager manager;

    [SerializeField]
    private float jumpTimer;

    void Start()
    {
        manager = gameObject.GetComponentInParent<PlayerManager>();
        jumpTimer = 0;
    }

    void Update()
    {
        if (!manager.entity.playerInitiateJump)
        {
            Move();
        }
        else
        {
            manager.playerRigidbody.velocity = Vector2.zero;
        }
        Jump();
        Height();
        Dash();
    }

    private void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            manager.entity.playerIsWalking = false;
            manager.entity.playerIsRunning = false;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (manager.entity.playerIsGrounded)
                {
                    manager.entity.playerIsWalking = false;
                    manager.entity.playerIsRunning = true;
                }
                else
                {
                    manager.entity.playerIsWalking = false;
                    manager.entity.playerIsRunning = false;
                }
            }
            else
            {
                if (manager.entity.playerIsGrounded)
                {
                    manager.entity.playerIsWalking = true;
                    manager.entity.playerIsRunning = false;
                }
                else
                {
                    manager.entity.playerIsWalking = false;
                    manager.entity.playerIsRunning = false;
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (manager.entity.playerIsGrounded)
            {
                manager.playerRigidbody.velocity = Vector2.right * xMove * manager.entity.playerRunSpeed + Vector2.up * yMove * manager.entity.playerRunSpeed;
            }
            else
            {
                if (!manager.entity.playerIsRunJumping)
                {
                    manager.playerRigidbody.velocity = Vector2.right * xMove * manager.entity.playerWalkSpeed + Vector2.up * yMove * manager.entity.playerWalkSpeed;
                }
                else
                {
                    manager.playerRigidbody.velocity = Vector2.right * xMove * manager.entity.playerRunSpeed + Vector2.up * yMove * manager.entity.playerRunSpeed;
                }
            }
        }
        else
        {
            if (!manager.entity.playerIsRunJumping)
            {
                manager.playerRigidbody.velocity = Vector2.right * xMove * manager.entity.playerWalkSpeed + Vector2.up * yMove * manager.entity.playerWalkSpeed;
            }
            else
            {
                manager.playerRigidbody.velocity = Vector2.right * xMove * manager.entity.playerRunSpeed + Vector2.up * yMove * manager.entity.playerRunSpeed;
            }
        }
    }

    private void Jump()
    {
        if (manager.entity.playerJumpMode)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                manager.entity.playerInitiateJump = true;

                jumpTimer += Time.deltaTime;

                if (jumpTimer >= manager.entity.playerJumpTimer)
                {
                    jumpTimer = manager.entity.playerJumpTimer;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                manager.entity.playerInitiateJump = false;
                manager.entity.playerIsJumping = true;
                manager.entity.playerIsGrounded = false;
            }

            if (manager.entity.playerIsJumping && !manager.entity.playerIsGrounded)
            {
                if (jumpTimer > 0)
                {
                    if (jumpTimer < manager.entity.playerJumpTimer / 2)
                    {
                        if (manager.playerTransform.position.y - manager.entity.shadow.transform.position.y <= manager.entity.playerMinJumpDistance)
                        {
                            manager.playerRigidbody.velocity += Vector2.up * manager.entity.playerJumpSpeed;
                        }
                        else
                        {
                            jumpTimer = 0;
                            manager.entity.playerIsJumping = false;
                        }
                    }
                    else if (jumpTimer < manager.entity.playerJumpTimer / 1.5 && jumpTimer >= manager.entity.playerJumpTimer / 2)
                    {
                        if (manager.playerTransform.position.y - manager.entity.shadow.transform.position.y <= manager.entity.playerMidJumpDistance)
                        {
                            manager.playerRigidbody.velocity += Vector2.up * manager.entity.playerJumpSpeed;
                        }
                        else
                        {
                            jumpTimer = 0;
                            manager.entity.playerIsJumping = false;
                        }
                    }
                    else
                    {
                        if (manager.playerTransform.position.y - manager.entity.shadow.transform.position.y <= manager.entity.playerMaxJumpDistance)
                        {
                            manager.playerRigidbody.velocity += Vector2.up * manager.entity.playerJumpSpeed;
                        }
                        else
                        {
                            jumpTimer = 0;
                            manager.entity.playerIsJumping = false;
                        }
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (manager.entity.playerIsGrounded)
                {
                    if (manager.entity.playerIsRunning)
                    {
                        manager.entity.playerIsRunJumping = true;
                    }
                    manager.entity.playerIsJumping = true;
                    manager.entity.playerIsGrounded = false;
                }
            }

            if (manager.entity.playerIsJumping && !manager.entity.playerIsGrounded)
            {
                if (manager.playerTransform.position.y - manager.entity.shadow.transform.position.y <= manager.entity.playerMinJumpDistance)
                {
                    manager.playerRigidbody.velocity += Vector2.up * manager.entity.playerJumpSpeed;
                }
                else
                {
                    manager.entity.playerIsJumping = false;
                }
            }
        }

        if (!manager.entity.playerIsJumping && !manager.entity.playerIsGrounded)
        {
            manager.playerRigidbody.velocity += Vector2.down * manager.entity.physicsManager.gravity;

            if (manager.playerTransform.position.y - manager.entity.shadow.transform.position.y <= 0f)
            {
                manager.playerTransform.position = new Vector3(manager.entity.shadow.transform.position.x, manager.entity.shadow.transform.position.y, manager.playerTransform.position.z);

                manager.entity.playerIsGrounded = true;
                manager.entity.playerIsRunJumping = false;
            }
        }
    }
    
    private void Height()
    {
        manager.entity.virtualZ = manager.playerTransform.position.y - manager.entity.shadow.transform.position.y + manager.entity.shadowBehaviour.virtualZ;
    }

    private void Dash()
    {
        
    }
}
