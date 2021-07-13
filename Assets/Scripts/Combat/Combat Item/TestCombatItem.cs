using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCombatItem : MonoBehaviour, ICombatItems
{
    private CombatItemType item = CombatItemType.C4;
    public bool CanUse()
    {
        return true;
    }

    public float GetCoolDownTime()
    {
        return 2f;
    }

    public void ResetUse()
    {
        
    }

    public void UseItem()
    {
        Debug.Log("use " + item.ToString());
    }
}
