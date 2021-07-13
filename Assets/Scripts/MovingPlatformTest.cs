using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTest : MonoBehaviour
{
    public Transform destination1;
    public Transform destination2;
    private Transform destination;
    private Rigidbody2D rb;
    public float speed;

    Vector2 direction = Vector2.zero;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        destination = destination1;
    }

    private void FixedUpdate()
    {

        if (Vector2.Distance(transform.position, destination1.position) < 0.1f)
        {
            destination = destination2;

        }

        if (Vector2.Distance(transform.position, destination2.position) < 0.1f)
        {
            destination = destination1;

        }

        direction = destination.position - transform.position;
        direction = direction.normalized;

        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        /*transform.position = Vector2.Lerp(transform.position, destination.position, speed * Time.deltaTime);*/
        //rb.MovePosition(Vector2.Lerp(transform.position, destination.position, speed * Time.deltaTime));
    }

    public Vector2 GetVelocity()
    {
        return new Vector2(direction.x * speed, direction.y * speed);
    }

    public Rigidbody2D GetRb()
    {
        return rb;
    }
}
