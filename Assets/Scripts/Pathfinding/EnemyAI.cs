using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public string targetTag = "Player";
    public Transform target;
    public bool targetAvailable = true;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    [Header("Telemtry")]
    public Vector2 directionTele;
    public Vector2 realDirectionTele;
    public Vector2 directionDiffTele;

    public bool stopTest;

    public bool stopChase = false;


    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();



        if (GameObject.FindGameObjectWithTag(targetTag) != null)
        {
            target = GameObject.FindGameObjectWithTag(targetTag).transform;
            targetAvailable = true;
        }
        else
        {
            targetAvailable = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

        if(targetAvailable) InvokeRepeating("UpdatePath", 0f, 0.5f);

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        if(stopTest)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        directionTele = direction;

        Vector2 force = direction * speed * Time.deltaTime;

        //rb.AddForce(force, ForceMode2D.Impulse);
        if (!stopTest)
        {
            if (!stopChase)
            {
                rb.velocity = force;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
            
        }
        
        

        realDirectionTele = (rb.velocity).normalized;
        directionDiffTele = realDirectionTele - directionTele;

        Debug.DrawLine(transform.position, new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z));

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }

    


}
