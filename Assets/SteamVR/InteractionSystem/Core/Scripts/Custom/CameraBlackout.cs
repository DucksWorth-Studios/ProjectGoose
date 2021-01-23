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

    #region TeleportSettings

    [Header("Teleport blackout settings")] 
    public bool enableTeleportBlackout;
    
    [Tooltip("In seconds, the amount of time the blackout should last for")]
    public float teleportBlackoutDuration = 1;
    
    #endregion TeleportSettings
    
    #region SnapTurnSettings

    [Header("Snap turn blackout settings")] 
    public bool enableSnapTurnBlackout;
    
    [Tooltip("In seconds, the amount of time the blackout should last for")]
    public float snapTurnBlackoutDuration = 1;
    
    #endregion SnapTurnSettings
    
    void Start()
    {
        instance = this;
    }

    public void TriggerTeleportBlackout()
    {
        // https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity
        if(enableTeleportBlackout)
            StartCoroutine(Blackout(teleportBlackoutDuration));
    }
    
    public void TriggerSnapTurnBlackout()
    {
        if(enableSnapTurnBlackout)
            StartCoroutine(Blackout(snapTurnBlackoutDuration));
    }

    private IEnumerator Blackout(float blackoutDuration)
    {
        SteamVR_Fade.View(Color.black, 0);
        yield return new WaitForSeconds(blackoutDuration);
        SteamVR_Fade.View(Color.clear, 0);
    }
}
