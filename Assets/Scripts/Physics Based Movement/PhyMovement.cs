using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyMovement : MonoBehaviour
{
    public float speed = 10f;
    public float maxVelocityChange = 10f;
    private Rigidbody2D rig;

    [Header("Dashing")]
    public KeyCode dashButton = KeyCode.Space;
    public float dashForce = 30f;

    [Header("Telemtry")]
    public Vector2 dir_dos;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        targetVelocity = transform.TransformDirection(targetVelocity);
        dir_dos = targetVelocity;
        targetVelocity *= speed;

        

        Vector2 velocity = rig.velocity;
        Vector2 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
        rig.AddForce(velocityChange, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(dashButton))
        {
            Dashing();
        }
    }

    public void Dashing()
    {
        Debug.Log("Dashing!!!");
        //rig.velocity = transform.TransformDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")))*dashForce;
        rig.AddForce(transform.TransformDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))) * dashForce,ForceMode2D.Impulse);
    }


}
