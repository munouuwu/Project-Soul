using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;


    private float sliderTarget;

    [SerializeField]
    float sliderSpeed;

    private Coroutine currentCor;

    private void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();
    }

    public void UpdateDashCharge(float chargeFraction)
    {
        /*sliderTarget = chargeFraction;
        if (currentCor == null) StartCoroutine(FillAnimation());*/
    }

    public IEnumerator FillAnimation()
    {
        while (slider.value != sliderTarget)
        {
            slider.value = Mathf.MoveTowards(slider.value, sliderTarget, sliderSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
