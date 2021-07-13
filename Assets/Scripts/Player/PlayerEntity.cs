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
    public float playerMinJumpDistance;
    public float playerJumpTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
