using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class AStarPathfindingAgent : MonoBehaviour
    {
        [Header("Target")]
        public string targetTag = "Player";
        private Vector2 targetPosition;

        public Transform targetTransform;
        public bool targetAvailable = true;

        [Header("AI Movement")]
        public float speed = 200f;
        public float nextWaypointDistance = 3f;

        Path path;
        int currentWaypoint = 0;
        bool reachedEndOfPath = false;

        Seeker seeker;
        Rigidbody2D rb;

        bool disableMovement;
        public bool MovementDisabled => disableMovement;

        private void OnEnable()
        {
            
        }

        void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();


            targetTransform = GameObject.FindGameObjectWithTag(targetTag).transform;
            targetAvailable = false;

            if (targetTransform != null)
            {
                targetAvailable = true;
                InvokeRepeating("UpdatePath", 0f, 0.5f); //ganti Coroutine because will still run at runtime
                InvokeRepeating("UpdateTarget", 0f, 20f);
            }
        }

        IEnumerator UpdatePathCorroutine(float delay)
        {
            while(true)
            {
                if (seeker.IsDone())
                {
                    seeker.StartPath(rb.position, targetTransform.position, OnPathComplete);
                }
                yield return delay;
            }    
            
        }

        IEnumerator UpdateTargetCoroutine(float delay)
        {
            while (true)
            {
                var targetTemp = GameObject.FindGameObjectWithTag(targetTag).transform;
                if (targetTemp != null)
                {
                    targetTransform = targetTemp;
                }
                yield return new WaitForSeconds(delay);
            }
        }

        void UpdateTarget()
        {
            var targetTemp = GameObject.FindGameObjectWithTag(targetTag).transform;
            if (targetTemp != null)
            {
                targetTransform = targetTemp;
            }
        }

        void UpdatePath()
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, targetTransform.position, OnPathComplete);
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

            Movement();

        }

        //MovementScript?

        private void Movement()
        {
            if (reachedEndOfPath || disableMovement) return;
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 magnitude = direction * speed * Time.deltaTime;

            rb.velocity = magnitude;


            Debug.DrawLine(transform.position, new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z));

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            Debug.DrawLine(transform.position, new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z));


        }

        public void StopMovement(bool stop)
        {
            disableMovement = stop;
            rb.velocity = Vector2.zero;

        }

        public void CancelPathfinding()
        {
            seeker.CancelCurrentPathRequest(false);
        }
    }
}

