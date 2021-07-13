using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeadPickUpPzl : MonoBehaviour
{
    public PickUpPzl[] pupzl;
    public int score;
    public int maxScore => pupzl.Length;

    public bool isCompleted = false;
    public Action OnHeadPUPzlComplete;

    // Start is called before the first frame update
    void Start()
    {
        foreach(PickUpPzl pup in pupzl)
        {
            pup.OnCollectedEvent -= AddScore;
            pup.OnCollectedEvent += AddScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {

        score++;
        if(score == maxScore)
        {
            isCompleted = true;
            OnHeadPUPzlComplete.Invoke();
        }
    }
}
