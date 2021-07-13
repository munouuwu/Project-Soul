using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPowerSystem : MonoBehaviour
{
    public bool active;

    public Transform muzzlePoint;

    public float pullSpeed;
    public float pushSpeed;
    public float maxMag = 5f;
    public float maxMass = 5f;
    public float maxDistance = 50f;

    public Rigidbody2D[] rigObjects;
    public int coAmount;


    public Vector2 moveDir;

    public int mode = 0;


    /*public HeadAimDirection headaim;*/

    // Start is called before the first frame update
    void Start()
    {
        /*if (headaim == null && GetComponent<HeadAimDirection>() != null)
        {
            headaim = GetComponent<HeadAimDirection>();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
       /* if (active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mode == 0)
                {
                    ClickOnRigid();
                }
                else if (mode == 1)
                {
                    PushObject();
                }
            }
        }*/
    }

   /*public void PullPush(Vector2 mousePoint)
    {
        if (mode == 0)
        {
            ClickOnRigid(mousePoint);
        }
        else if (mode == 1)
        {
            PushObject();
        }
    }*/

    

    private void FixedUpdate()
    {
        
        /*if (mode == 1 && rigObject != null)
        {
            moveDir = new Vector2(muzzlePoint.position.x - rigObject.position.x, muzzlePoint.position.y - rigObject.position.y);
            //rigObject.velocity = moveDir * pullSpeed * (1/rigObject.mass);
            rigObject.velocity = Vector3.ClampMagnitude(moveDir, maxMag) * pullSpeed * (1 / rigObject.mass);

            *//*if (!active)
            {
                DropObject();
            }*//*
        }*/

        for(int i = 0; i < coAmount; i++)
        {
            moveDir = new Vector2(muzzlePoint.position.x - rigObjects[i].position.x, muzzlePoint.position.y - rigObjects[i].position.y);
            //rigObject.velocity = moveDir * pullSpeed * (1/rigObject.mass);
            rigObjects[i].velocity = Vector3.ClampMagnitude(moveDir, maxMag) * pullSpeed * (1 / rigObjects[i].mass);
        }
    }

    public void DropObject()
    {
        /*rigObjects = null;
        mode = 0;*/

        for (int i = 0; i < coAmount; i++)
        {
            rigObjects[i] = null;
        }

        coAmount = 0;
    }

    void ClickOnRigid(Vector2 mousePoint)
    {

        if (active)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);

            if (coAmount < rigObjects.Length)
            {
                if (hit.rigidbody != null && hit.collider != null)
                {
                    if (Vector2.Distance(hit.transform.position, transform.position) <= maxDistance)
                    {
                        //Disable for fix. Fix is bellow
                        /*if (hit.transform.GetComponent<ThrowableObject>() != null)
                        {
                            if (hit.transform.GetComponent<ThrowableObject>().isPullAble)
                            {
                                if (hit.rigidbody.mass <= maxMass)
                                {
                                    //rigObject = hit.transform.GetComponent<Rigidbody2D>();
                                    //mode = 1;

                                    rigObjects[coAmount] = hit.transform.GetComponent<Rigidbody2D>();
                                    coAmount++;
                                }
                            }
                        }*/

                        //start fix
                        ThrowableObject throwable = hit.transform.GetComponent<ThrowableObject>();
                        if (throwable != null && throwable.isPullAble)
                        {
                            if (hit.rigidbody.mass <= maxMass)
                            {
                                if (FindObjectOfType<AudioManager>() != null)
                                {
                                    FindObjectOfType<AudioManager>().Play("Pull");
                                }
                                //rigObject = hit.transform.GetComponent<Rigidbody2D>();
                                //mode = 1;

                                rigObjects[coAmount] = hit.transform.GetComponent<Rigidbody2D>();

                                //Temporary fix
                                KamikazeBehaviour ht = hit.transform.GetComponent<KamikazeBehaviour>();

                                if (ht != null)
                                {
                                    ht.stopMovement = true;
                                    hit.transform.GetComponent<EnemyPathfinding>().StopMovement(true);
                                }
                                //end of Temporary fix

                                throwable.isPulled = true;
                                coAmount++;


                            }
                        }

                        //End of Fix

                    }
                }
            }
        }
        

        
    }

    public void PullObject(Vector2 mousePoint)
    {
        ClickOnRigid(mousePoint);
    }

    public void PushObject()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("Push");
        }
        /*rigObject.velocity = muzzlePoint.TransformDirection(0, pushSpeed*(1/rigObject.mass), 0);

        rigObject = null;
        mode = 0;*/

        for (int i = 0; i < coAmount; i++)
        {
            rigObjects[i].velocity = muzzlePoint.TransformDirection(0, pushSpeed * (1 / rigObjects[i].mass), 0);

            //Start Fix
            rigObjects[i].GetComponent<ThrowableObject>().isPulled = false;
            //End Fix
            rigObjects[i] = null;

            
        }

        coAmount = 0;
    }

    

}
