using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldSystem : MonoBehaviour
{
    EnemyAI ai;
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

    public Animator bossTor;
    public float torDir;

    public bool isDead;

    public float rotateSpeed = 30f;
    public Transform lateHead;

    public Vector2 alphaLevel = new Vector2(0.2f, 1f);
    public float colorRecoverSpeed = 10f;
    Health bossHealth;

    public SpriteRenderer bossSprite;


    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<EnemyAI>();
        delayFire = Random.Range(fireInterval.x, fireInterval.y);
        bossHealth = GetComponent<Health>();

        if (GameObject.FindGameObjectWithTag(targetTag) != null)
        {
            target = GameObject.FindGameObjectWithTag(targetTag).transform;
            targetAvailable = true;
        }
        else
        {
            targetAvailable = false;
        }

        bossHealth.OnDamageEvent -= OnDamageTaken;
        bossHealth.OnDamageEvent += OnDamageTaken;

        bossHealth.OnDeathEvent -= OnDeath;
        bossHealth.OnDeathEvent += OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        bossSprite.color = new Color(bossSprite.color.r, bossSprite.color.g, bossSprite.color.b, Mathf.Lerp(bossSprite.color.a,alphaLevel.y,colorRecoverSpeed*Time.deltaTime));

        if (!isDead)
        {
            if (targetAvailable)
            {
                vectorDir = target.position - transform.position;

                aimDir = Mathf.Atan2(vectorDir.y, vectorDir.x) * Mathf.Rad2Deg - 90f;
                head.rotation = Quaternion.Euler(new Vector3(0, 0, aimDir));

                torDir = target.position.x - this.transform.position.x;
                bossTor.SetFloat("direction", (int)Mathf.Clamp01((float)torDir));

                lateHead.rotation = Quaternion.RotateTowards(lateHead.rotation, head.rotation, rotateSpeed * Time.deltaTime);

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
                        if (fireSFX != null)
                        {
                            fireSFX.Play();
                        }

                        nttf = Time.time + 1f / (fireRateRPM / 60f);
                    }
                }
            }
        }
        else
        {
            ai.stopChase = true;
            bossTor.SetTrigger("Die");
        }
        

    }

    void OnDamageTaken(float damage)
    {
        Debug.Log("BOSS TAKING DAMAGE");
        bossSprite.color = new Color(bossSprite.color.r, bossSprite.color.g, bossSprite.color.b, alphaLevel.x);
    }

    void OnDeath()
    {
        isDead = true;
    }

    void ShootProjectile()
    {
        //GameObject bullet = Instantiate(projectile, muzzlePoint.position, Quaternion.Euler(new Vector3(muzzlePoint.rotation.x,muzzlePoint.rotation.y,muzzlePoint.rotation.z+Random.Range(-inAccuracy,inAccuracy))));
        GameObject bullet = Instantiate(projectile[Random.Range(0, projectile.Length)], muzzlePoint.position, Quaternion.Euler(new Vector3(muzzlePoint.eulerAngles.x, muzzlePoint.eulerAngles.y, muzzlePoint.eulerAngles.z + Random.Range(-inAccuracy, inAccuracy))));
        //GameObject bullet = Instantiate(projectile, muzzlePoint.position, muzzlePoint.rotation);
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
        rig.velocity = bullet.transform.TransformDirection(0, bulletSpeed, 0);
        BulletEnemy b_e = bullet.GetComponent<BulletEnemy>();
        b_e.target = target;
        b_e.bulletSpeed = bulletSpeed;


    }

}
