using UnityEngine;

/// <summary>
/// Author: Cameron Scholes
/// Holds references to teleport points
/// </summary>

public class TeleportPoint : MonoBehaviour
{
    private static int noOfPoints = 5;
    private static int count = 0;
    public static GameObject[] points;
    
    void Start()
    {
        if (points == null)
            points = new GameObject[noOfPoints];

        points[count] = gameObject;
        count++;
        
        // print(count);
        // print(points);
    }
}
