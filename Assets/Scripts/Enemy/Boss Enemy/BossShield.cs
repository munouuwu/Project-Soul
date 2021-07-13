using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    public int NumberOfOrb;
    public int orbDestroyed;

    private void OnEnable()
    {
        orbDestroyed = 0;
    }

    public void OrbDestroyed()
    {
        Debug.Log("orb destroy updated");
        orbDestroyed = orbDestroyed + 1;

        if (orbDestroyed == NumberOfOrb)
        {
            transform.tag = "Enemy";
        }
    }
}
