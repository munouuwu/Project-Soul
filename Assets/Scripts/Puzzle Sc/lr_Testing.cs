using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_Testing : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineRendererR line;

    private void Start()
    {
        line.SetUpLine(points);
    }
}
