using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] float healthPoints = 100;
    [SerializeField] float maxHealth = 100;
    
    public float CurrentHP => healthPoints;
    public float MaxHP => maxHealth;

    private bool dead;
    public bool IsDead { get { return dead; } }

    public bool DestroyOnDeath = false;

    public Action OnDeathEvent;
    public Action<float> OnDamageEvent;
    public Action<float> OnHealEvent;

    

    public void TakeDamage(float damage)
    {

        if (dead) return;
        healthPoints = Mathf.Max(healthPoints - damage, 0);
        OnDamage(damage);

        if ((int)healthPoints != 0) return;
        OnDeath();
    }
    

    private void OnDamage(float damage)
    {
        if (OnDamageEvent == null) return;
        OnDamageEvent.Invoke(damage);
    }

    private void OnDeath()
    {
        if (dead) return;

        dead = true;

        //Debug.Log("mati");
        if (OnDeathEvent != null)
            OnDeathEvent.Invoke(); // Invoke = broadcast

        

        if (DestroyOnDeath) Destroy(gameObject);
    }

    public float Heal(float points)
    {
        
        healthPoints = Mathf.Min(healthPoints + points, maxHealth);
        if(OnHealEvent != null)
        {
            OnHealEvent.Invoke(CurrentHP);
        }
        return CurrentHP;
    }

    public void Ressurect(float points)
    {
        dead = false;
        Debug.Log("PLAYER RESURRECTED =" + dead);
        healthPoints = points;
        if (OnHealEvent != null)
        {
            OnHealEvent.Invoke(CurrentHP);
        }
    }

    
}
