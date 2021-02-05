using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public static int noOfPoints = 5;
    public static int count = 0;
    public static GameObject[] points;
    
    void Start()
    {
        if (points == null)
            points = new GameObject[noOfPoints];

        points[count] = gameObject;
        count++;
        
        print(count);
        print(points);
    }
}
