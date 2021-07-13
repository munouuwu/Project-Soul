using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringItemPzl : MonoBehaviour
{
    public int scoreUniv;

    [System.Serializable]
    public class BringItemSystem
    {
        public Transform homeObject;
        public GameObject targetObject;
        public float minDistance = 0.1f;
        public float distance;
        public bool collected;
        public int score = 0;

        public void InUpdate()
        {


            distance = Vector2.Distance(homeObject.position, targetObject.transform.position);

            if (!collected)
            {
                if (distance <= minDistance)
                {
                    targetObject.SetActive(false);
                    score = 1;
                    collected = true;

                    
                }
            }



        }




    }

    public BringItemSystem[] bis;

    public int[] perItemScore;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void CheckScore()
    {

    }
}




