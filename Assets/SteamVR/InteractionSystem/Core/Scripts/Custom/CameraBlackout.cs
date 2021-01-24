using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Author: Cameron Scholes
/// Script to blackout the camera when teleporting
/// </summary>

public class CameraBlackout : MonoBehaviour
{
    public static CameraBlackout instance;

    void Start()
    {
        instance = this;
    }

    public void TriggerTeleportBlackout()
    {
        // https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity
        if(ComfortManager.settingsData.enableTeleportBlackout == 1)
            StartCoroutine(Blackout(ComfortManager.settingsData.tpBlackoutDuration));
    }
    
    public void TriggerSnapTurnBlackout()
    {
        if(ComfortManager.settingsData.enableSnapTurnBlackout == 1)
            StartCoroutine(Blackout(ComfortManager.settingsData.stBlackoutDuration));
    }

    private IEnumerator Blackout(float blackoutDuration)
    {
        SteamVR_Fade.View(Color.black, 0);
        yield return new WaitForSeconds(blackoutDuration);
        SteamVR_Fade.View(Color.clear, 0);
    }
}
