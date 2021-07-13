using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public string targetTag = "Player";
    public bool targetAvailable = true;
    public Transform target;
    public GameObject[] projectile;
    public float fireRateRPM;
    float nttf;
    public float bulletSpeed = 10f;

    public Vector2 vectorDir;
    public float aimDir;

    public float inAccuracy;

    public int attackVariance;

    //HeadAI
    public Transform head;
    public Transform muzzlePoint;

    [Header("behavior")]
    public Vector2 fireInterval;
    public Vector2 fireHoldTime;
    public bool canFire = true;
    public bool isFiring = false;
    public float delayFire;
    public AudioSource fireSFX;


    // Start is called before the first frame update
    void Start()
    {
        delayFire = Random.Range(fireInterval.x, fireInterval.y);

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

    // Update is called once per frame
    void Update()
    {
        if (targetAvailable)
        {
            vectorDir = target.position - transform.position;
            
            aimDir = Mathf.Atan2(vectorDir.y, vectorDir.x) * Mathf.Rad2Deg - 90f;

            head.rotation = Quaternion.Euler(new Vector3(0, 0, aimDir));

            if (canFire)
            {
                if (delayFire > 0)
                {
                    delayFire -= 1f * Time.deltaTime;
                }
                else
                {
                    isFiring = !isFiring;
                    switch (isFiring)
                    {
                        case true:
                            delayFire = Random.Range(fireHoldTime.x, fireHoldTime.y);
                            break;
                        case false:
                            delayFire = Random.Range(fireInterval.x, fireHoldTime.y);
                            break;
                    }
                }

                if (isFiring && Time.time >= nttf)
                {
                    ShootProjectile();
                    if(fireSFX != null)
                    {
                        fireSFX.Play();
                    }
                    
                    nttf = Time.time + 1f / (fireRateRPM / 60f);
                }
            }
        }
        
    }

    void ShootProjectile()
    {
        //GameObject bullet = Instantiate(projectile, muzzlePoint.position, Quaternion.Euler(new Vector3(muzzlePoint.rotation.x,muzzlePoint.rotation.y,muzzlePoint.rotation.z+Random.Range(-inAccuracy,inAccuracy))));
        GameObject bullet = Instantiate(projectile[Random.Range(0,projectile.Length)], muzzlePoint.position, Quaternion.Euler(new Vector3(muzzlePoint.eulerAngles.x, muzzlePoint.eulerAngles.y, muzzlePoint.eulerAngles.z + Random.Range(-inAccuracy, inAccuracy))));
        //GameObject bullet = Instantiate(projectile, muzzlePoint.position, muzzlePoint.rotation);
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
        rig.velocity = bullet.transform.TransformDirection(0, bulletSpeed, 0);
        BulletEnemy b_e = bullet.GetComponent<BulletEnemy>();
        b_e.target = target;
        b_e.bulletSpeed = bulletSpeed;
        

    }

    

    /*void ShootProjectile(float flyt)
    {
        GameObject bullet = Instantiate(projectile, muzzlePoint.position, muzzlePoint.rotation);
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
        //rig.velocity = muzzlePoint.TransformDirection(0, bulletSpeed, bulletSpeed);
        rig.velocity = muzzlePoint.TransformDirection(0, bulletSpeed, 0);
        bullet.GetComponent<KnifeLocal>().flyTime = flyt;
    }*/
}
