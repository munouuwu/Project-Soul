using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealhUI : MonoBehaviour
{
    [SerializeField] Slider uiSlider;
    [SerializeField] Health health;

    private void Start()
    {
        uiSlider.maxValue = health.MaxHP;
        uiSlider.value = health.MaxHP;
        health.OnDamageEvent -= OnDamage;
        health.OnDamageEvent += OnDamage;
        health.OnHealEvent -= OnHeal;
        health.OnHealEvent += OnHeal;
    }

    private void OnHeal(float points)
    {
        uiSlider.value = health.CurrentHP;
    }
    private void OnDamage(float damage)
    {
        uiSlider.value = health.CurrentHP;
        
    }
}
