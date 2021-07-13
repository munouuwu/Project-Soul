using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerManager manager;

    [SerializeField]
    private float jumpTimer;

    [SerializeField]
    private Vector2D shadowPosition;
    [SerializeField]
    private float beforeShadowPosition;
    [SerializeField]
    private float afterShadowPosition;

    void Start()
    {
        manager = gameObject.GetComponentInParent<PlayerManager>();
        jumpTimer = manager.entity.playerJumpTimer;
    }

    void Update()
    {
        Move();
        Jump();
        Dash();
    }

    private void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            manager.playerRigidbody.velocity = Vector2.right * xMove * manager.entity.playerRunSpeed + Vector2.up * yMove * manager.entity.playerRunSpeed;
        }
        else
        {
            manager.playerRigidbody.velocity = Vector2.right * xMove * manager.entity.playerWalkSpeed + Vector2.up * yMove * manager.entity.playerWalkSpeed;
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            beforeShadowPosition = manager.playerTransform.position.y;
            manager.playerRigidbody.velocity = Vector2.up * manager.entity.playerJumpSpeed;
            afterShadowPosition = manager.playerTransform.position.y;

            /*shadowPosition = new Vector2D(manager.playerTransform.position.x, );

            if ()
            {

            }
            else
            {

            }*/
        }
        else
        {
            shadowPosition = new Vector2D(manager.playerTransform.position.x, manager.playerTransform.position.y);
        }
    }

    private void Dash()
    {

    }
}
