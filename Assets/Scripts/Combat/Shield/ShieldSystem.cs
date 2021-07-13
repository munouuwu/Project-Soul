using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSystem : MonoBehaviour, ShieldSysten
{
    private bool shieldUp;
    private bool shieldAttacked;

    [SerializeField]
    private float regenDelay = 2;
    private float regenDelayTimer = 0;

    [SerializeField]
    float regenRate = 1f;

    [SerializeField]
    Transform shield;
    [SerializeField] 
    ShieldUI shieldUI;
  

    private Health health;
    private float maxHealth;

    SpriteRenderer sprite;

    

    private void Start()
    {
        sprite = shield.GetComponent<SpriteRenderer>();
        health = shield.GetComponent<Health>();

        maxHealth = health.CurrentHP;
        health.OnDamageEvent += OnDamage;

    }

    
    private void Update()
    {
        if(shieldAttacked)
        {
            regenDelayTimer += Time.deltaTime;
            if(regenDelayTimer >= regenDelay)
            {
                shieldAttacked = false;
                regenDelayTimer = 0;
            }
        }
        else
        {
            if(health.CurrentHP != maxHealth)
            {
                health.Heal(regenRate*Time.deltaTime);
                UpdateShieldHealth();
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.T) && shieldUp)
        {
            health.TakeDamage(5);
            
            OnDamage(5);
        }
    }

    public void ShieldsUp()
    {
        shieldUp = true;
        regenDelayTimer = 0;
        shield.gameObject.SetActive(true);
    }

    

    public void ShieldsDown()
    {
        shieldUp = false;
        if (gameObject.activeInHierarchy)
            shield.gameObject.SetActive(false);
    }

    public void DoSkill(Vector2 directionInput)
    {
        /*//Ngeluarin Shield
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("buka");
            shieldUp = true;
            regenDelayTimer = 0;
            if(!gameObject.activeInHierarchy)
            {
                Debug.Log("uwaw");
                
            }
            shield.gameObject.SetActive(true);

        }
        else
        {
            shieldUp = false;
            if(gameObject.activeInHierarchy)
                shield.gameObject.SetActive(false);
            Debug.Log("tutup");
        }*/
        

    }

    public bool isDoingSkill()
    {
        return shieldUp;
    }
    
    private void OnDamage(float damage)
    {
        shieldAttacked = true;
        UpdateShieldHealth();
    }
    private void UpdateShieldHealth()
    {
        
        shieldUI.UpdateShieldHealth(health.CurrentHP);
        float temp = health.CurrentHP / health.MaxHP * 255;
        //sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, temp);
        
    }

    


}
