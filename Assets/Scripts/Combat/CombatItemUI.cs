using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatItemUI : MonoBehaviour
{
    public Image imageUI;
    public GameObject combatItemObj;
    public ICombatItems combatItem;
    bool onCooldown;
    public bool OnCooldown => OnCooldown;

    private void Awake()
    {
        combatItem = combatItemObj.GetComponent<ICombatItems>();
    }
    public void UseItem()
    {

        if (onCooldown) return;
        if (!combatItem.CanUse()) return;
        onCooldown = true;
        combatItem.UseItem();
        UpdateUI(0);
        StartCoroutine(ItemCoolDown(combatItem.GetCoolDownTime()));
    }
    public IEnumerator ItemCoolDown(float duration)
    {
        float cooldownTime = duration;//combatItem.GetCoolDownTime();
        float timer = 0;
        while(timer < cooldownTime)
        {
            
            timer = Mathf.Min(timer + Time.deltaTime,cooldownTime);
            
            UpdateUI(timer/cooldownTime);
            yield return null;
        }
        //if (!combatItem.CanUse()) combatItem.ResetUse();
        onCooldown = false;
        yield return null;
    }

    private void UpdateUI(float fraction)
    {
        imageUI.fillAmount = fraction;
    }
}

