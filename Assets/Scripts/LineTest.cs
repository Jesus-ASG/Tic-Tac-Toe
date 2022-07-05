using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{

    [SerializeField] private Transform[] points;
    [SerializeField] private LineGenerator line;

    
    void Start()
    {
        line.setupLine(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
