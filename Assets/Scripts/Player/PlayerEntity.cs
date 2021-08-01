using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [Header("Stats")]
    public int playerHp;
    public int playerMp;

    [Header("Movement")]
    public float playerWalkSpeed;
    public float playerRunSpeed;
    public float playerJumpSpeed;
    public float playerMaxJumpDistance;
    public float playerMidJumpDistance;
    public float playerMinJumpDistance;
    public float playerJumpTimer;
    public bool playerInitiateJump;
    public bool playerIsJumping;
    public bool playerIsGrounded;
    public bool playerIsWalking;
    public bool playerIsRunning;
    public bool playerIsRunJumping;
    public bool playerJumpMode;
    public GameObject shadow;
    public PlayerShadowBehaviour shadowBehaviour;
    public PhysicsManager physicsManager;

    public float virtualZ;

    // Start is called before the first frame update
    void Start()
    {
        shadowBehaviour = shadow.GetComponent<PlayerShadowBehaviour>();
        virtualZ = shadowBehaviour.virtualZ;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (Mathf.RoundToInt(transform.position.y * 100f) * -1) + (Mathf.RoundToInt(virtualZ * 100f));
    }
}
