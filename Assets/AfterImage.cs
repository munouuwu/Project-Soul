using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public float FadeOutTime = 1;
    private float fadeOutTimer = 0;

    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        fadeOutTimer = 0;
        
    }

    private void Update()
    {
        fadeOutTimer = Mathf.Min(fadeOutTimer + Time.deltaTime, FadeOutTime);
        
        if(fadeOutTimer == FadeOutTime)
        {
            gameObject.SetActive(false);
        }
        else
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1-(fadeOutTimer/FadeOutTime));
        }
    }
}
