using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowBehaviour : MonoBehaviour
{
    public GameObject playerEntity;
    private PlayerEntity entity;
    private Rigidbody2D rb;

    public PhysicsManager physicsManager;

    public float virtualZ;

    void Start()
    {
        entity = playerEntity.GetComponent<PlayerEntity>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        virtualZ = physicsManager.virtualBbottom;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Height();

        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }

    private void Move()
    {
        if (entity.playerIsGrounded)
        {
            transform.position = new Vector3(playerEntity.transform.position.x, playerEntity.transform.position.y, transform.position.z);
        }
        else
        {
            float xMove = Input.GetAxisRaw("Horizontal");
            float yMove = Input.GetAxisRaw("Vertical");

            if (!entity.playerInitiateJump)
            {
                if (!entity.playerIsRunJumping)
                {
                    rb.velocity = Vector2.right * xMove * entity.playerWalkSpeed + Vector2.up * yMove * entity.playerWalkSpeed;
                }
                else
                {
                    rb.velocity = Vector2.right * xMove * entity.playerRunSpeed + Vector2.up * yMove * entity.playerRunSpeed;
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void Height()
    {

    }
}
