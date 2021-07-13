using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDestroy : MonoBehaviour
{
    public BossShield bossShield;
    public Health health;
    // Start is called before the first frame update

    [SerializeField] List<GameObject> objToSpawn;
    [SerializeField] List<GameObject> objToDespawn;


    void Awake()
    {
        health = gameObject.GetComponent<Health>();
        //bossShield = GetComponent<BossShield>();
        health.OnDeathEvent -= OnDeathEvent;
        health.OnDeathEvent += OnDeathEvent;

    }

   /* void Start()
    {
        health = gameObject.GetComponent<Health>();
        health.OnDeathEvent -= OnDeathEvent;
        health.OnDeathEvent += OnDeathEvent;
        
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ThrowableObject>() != null)
        {

            health.TakeDamage(50);
            getHit();
        }
    }

    private void OnEnable()
    {
        Debug.Log("Objek di Enable="+gameObject.name);
        if(health != null)
        {
            //Debug.Log("Health ADA =" + health + "DI GAMEOBJECT ="+gameObject.name);
            health.Ressurect(health.MaxHP);
        }
    }

    private void OnDeathEvent()
    {
        if (bossShield != null)
        {
            bossShield.OrbDestroyed();
        }

        foreach (GameObject obj in objToSpawn)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in objToDespawn)
        {
            obj.SetActive(!true);
        }


        //gameObject.SetActive(false);
    }

 

    void getHit()
    {
        Debug.Log("kena batu");
    }

}
