using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrow : MonoBehaviour
{
    public bool active;

    public Transform muzzlePoint; // bole
    public GameObject projectile; //bole
    public float bulletSpeed = 100f;

    float flyt;

    public int maxAmmo = 5;
    public int ammo = 5;


    //TEMPORARY MOUSE LOOK
    /*public HeadAimDirection headaim;*/

    //public Vector2 mousePoint;

    /*public Camera cam;
    float angle;*/
    

    // Start is called before the first frame update
    /*void Start()
    {
        *//*if (headaim == null && GetComponent<HeadAimDirection>() != null)
        {
            headaim = GetComponent<HeadAimDirection>();
        }*//*
    }*/

    /*// Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                flyt = Vector2.Distance(headaim.mousePoint, new Vector2(muzzlePoint.position.x, muzzlePoint.position.y)) / bulletSpeed;
                ShootProjectile(flyt);
            }
        }

    }*/

    public void ThrowKnife(Vector2 mousePoint)
    {
        if(ammo > 0)
        {
            if(FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play("Throw Knife");
            }
            flyt = Vector2.Distance(mousePoint, new Vector2(muzzlePoint.position.x, muzzlePoint.position.y)) / bulletSpeed;
            ShootProjectile(flyt);
            ammo--;
        }
    }

    void ShootProjectile(float flyt)
    {

        GameObject bullet = Instantiate(projectile,muzzlePoint.position,muzzlePoint.rotation);
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
        //rig.velocity = muzzlePoint.TransformDirection(0, bulletSpeed, bulletSpeed);
        rig.velocity = muzzlePoint.TransformDirection(0, bulletSpeed, 0);
        bullet.GetComponent<KnifeLocal>().flyTime = flyt;
    }

    public void AddAmmo()
    {
        ammo = Mathf.Min(ammo + 1, 5);
    }
}
