using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOutManager : MonoBehaviour
{
    public int heroIndex;
    public GameObject[] heroObject;
    public GameObject[] heroCore;
    public GameObject[] heroChangeObject;

    public CamFollow camFollow;

    // Start is called before the first frame update
    void Start()
    {
        camFollow.targetFollow = heroObject[heroIndex].transform;
        /*for (int i = 0; i < heroObject.Length; i++)
        {
            if (i == heroIndex)
            {
                heroObject[i].transform.position = heroChangeObject[i].transform.position;
                heroObject[i].SetActive(true);
                heroCore[i].SetActive(true);
                heroChangeObject[i].SetActive(false);
            }
            else
            {
                heroChangeObject[i].SetActive(true);
                heroObject[i].transform.position = heroChangeObject[i].transform.position;
                heroObject[i].SetActive(false);
                heroCore[i].SetActive(false);

            }
        }*/
        RefreshHeroObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHeroTo(int hindex)
    {
        heroIndex = hindex;

        RefreshHeroObject();

        camFollow.targetFollow = heroObject[heroIndex].transform;



    }

    public void RefreshHeroObject()
    {
        for (int i = 0; i < heroObject.Length; i++)
        {
            if (i == heroIndex)
            {
                heroObject[i].transform.position = heroChangeObject[i].transform.position;
                heroObject[i].SetActive(true);
                heroCore[i].SetActive(true);
                heroChangeObject[i].SetActive(false);



            }
            else
            {
                heroChangeObject[i].transform.position = heroObject[i].transform.position;
                heroChangeObject[i].SetActive(true);
                heroObject[i].SetActive(false);
                heroCore[i].SetActive(false);

            }
        }

        /*int temp = (heroIndex - 1 < 0) ? heroObject.Length - 1 : heroIndex - 1;
        heroCore[temp].SetActive(false); // disable hero lama
        heroObject[heroIndex].transform.position = heroObject[temp].transform.position;//fix posisi hero baru
        heroCore[heroIndex].SetActive(true); // enable hero baru
        heroChangeObject[temp].SetActive(true);//matiin changer lama
        heroChangeObject[heroIndex].SetActive(false);//nyalainChanger baru*/

    }
}
