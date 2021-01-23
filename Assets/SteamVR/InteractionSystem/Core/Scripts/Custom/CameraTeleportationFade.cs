using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Author: Cameron Scholes
/// Script to blackout the camera when teleporting
/// </summary>

public class CameraTeleportationFade : MonoBehaviour
{
    public static CameraTeleportationFade instance;

    [Header("Blackout settings")] 
    public bool enableBlackout;
    
    [Tooltip("In seconds, the amount of time the blackout should last for")]
    public float blackoutDuration = 1;
    
    void Start()
    {
        instance = this;
    }

    public void TriggerBlackout()
    {
        // https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity
        if(enableBlackout)
            StartCoroutine(Blackout());
    }

    private IEnumerator Blackout()
    {
        SteamVR_Fade.View(Color.black, 0);
        yield return new WaitForSeconds(blackoutDuration);
        SteamVR_Fade.View(Color.clear, 0);
    }
}
