using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUnlocker : MonoBehaviour
{
    public GravityPowerSystem skillToUnlock;
    public GameObject skillImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Witch Character OF")
        {
            skillToUnlock.active = true;
            this.gameObject.SetActive(false);
            skillImage.SetActive(true);
        }
    }
}
