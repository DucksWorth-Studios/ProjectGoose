using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public float pourThreshold = 35f;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream = null;


    private void Update()
    {

    }
    /// <summary>
    /// Author: Tomas
    /// Used to create the Stream from stream Class. When object goes below the titlt angle
    /// </summary>
    private void StartPour()
    {

    }
    /// <summary>
    /// Authro:Tomas
    /// Used to end the stream when object goes above the tiltangle
    /// </summary>
    private void EndPour()
    {

    }

    /// <summary>
    /// Autho: Tomas
    /// Used to calculate if the item is to begin pouring or not
    /// </summary>
    
    private bool CalculatePourAngle()
    {
        return false;
    }

    /// <summary>
    /// Used to create the Stream from the stream class.
    /// </summary>
    /// <returns></returns>
    private Stream CreateStream()
    {
        return null;
    }
}