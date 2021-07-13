using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatItemSystem : MonoBehaviour
{
    
    public List<ICombatItems> combatItems;

    [SerializeField]
    List<CombatItemUI> ItemUI;
    
    
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ItemUI[0].UseItem();
        }
        /*if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ItemUI[1].UseItem();
        }*/
    }

    public void UseItem(int slot)
    {
        
    }
}

public enum CombatItemType
{
    LandMine,
    C4
}
