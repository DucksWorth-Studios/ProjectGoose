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
        //are we at a valid angle to begin?
        bool pourCheck = CalculatePourAngle();

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
    }
    /// <summary>
    /// Author: Tomas
    /// Used to create the Stream from stream Class. When object goes below the titlt angle
    /// </summary>
    private void StartPour()
    {
        //Create a new Stream
        currentStream = CreateStream();
        currentStream.Begin();
    }
    /// <summary>
    /// Authro:Tomas
    /// Used to end the stream when object goes above the tiltangle
    /// </summary>
    private void EndPour()
    {
        //End streeam and destroy
        currentStream.End();
        currentStream = null;
    }

    /// <summary>
    /// Autho: Tomas
    /// Used to calculate if the item is to begin pouring or not
    /// </summary>
    private bool CalculatePourAngle()
    {
        //Is the angle past the tipping point?
        float angle = transform.forward.y * Mathf.Rad2Deg;
        if (angle < pourThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Used to create the Stream from the stream class.
    /// </summary>
    /// <returns></returns>
    private Stream CreateStream()
    {
        //generate a stream object
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }
}