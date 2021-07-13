using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICombatItems 
{
    public void UseItem();
    public float GetCoolDownTime();

    public bool CanUse();

    public void ResetUse();
}
