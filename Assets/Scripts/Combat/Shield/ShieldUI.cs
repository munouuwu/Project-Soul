using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    [SerializeField]
    Health health;
    [SerializeField]
    private Slider slider;

    private float sliderTarget;

    [SerializeField]
    float sliderSpeed;

    private Coroutine currentCor;

    private void Start()
    {
        if(slider == null)
            slider = GetComponent<Slider>();
        slider.maxValue = health.MaxHP;
        slider.value = health.MaxHP;
    }

    public void UpdateShieldHealth(float health)
    {
        slider.value = health;
        sliderTarget = health;
        if (currentCor == null)
        {
            currentCor =  StartCoroutine(FillAnimation());
        } 
        
        
    }

    public IEnumerator FillAnimation ()
    {
        while(slider.value != sliderTarget)
        {
            slider.value = Mathf.MoveTowards(slider.value, sliderTarget, sliderSpeed * Time.deltaTime);
            yield return null;
        }
        currentCor = null;
    }
}
