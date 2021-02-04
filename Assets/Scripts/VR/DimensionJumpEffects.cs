using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Valve.VR;

/// <summary>
/// Author: Andrew Carolan
/// Controls the camera fade and VFX for when the player uses the dimension jump
/// </summary>
public class DimensionJumpEffects : MonoBehaviour
{
    #region Variables
    //Camera Fade
    private Color fadeColor = Color.black;
    private float fadeDuration = 0.5f;

    //VFX variables
    private VisualEffect jumpEffect;

    [Tooltip("The amount of time the effect should play for")]
    private float effectPlayTime = 1f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        jumpEffect = GetComponent<VisualEffect>();

        if (jumpEffect != null)
            jumpEffect.Stop();

        //Event Subscription
        EventManager.instance.OnTimeJumpButtonPressed += CallCameraFade;
        EventManager.instance.OnTimeJump += CallVFX;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CallCameraFade()
    {
        StartCoroutine(StartCameraFade());
    }

    /// <summary>
    /// Fades the camera to black then back to clear for the dimension jump
    /// Based off Cameron's fade to white method in FadeCamera Class
    /// </summary>
    /// <see cref="FadeCamera.FadeToWhite"/>
    /// <returns>a yield return that waits for 0.5 seconds to allow for fade of camera</returns>
    private IEnumerator StartCameraFade()
    {
        //set start color
        SteamVR_Fade.View(Color.clear, 0f);

        //fade to black
        SteamVR_Fade.View(fadeColor, fadeDuration);

        //Wait for the fade to complete
        yield return new WaitForSeconds(0.5f);

        //fade back to clear
        SteamVR_Fade.View(Color.clear, fadeDuration);
    }

    /// <summary>
    /// Calls the IEnumerator function StartVFX
    /// </summary>
    private void CallVFX()
    {
        StartCoroutine(StartVFX());
    }

    /// <summary>
    /// Starts the VFX after the player has completed the dimension jump
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartVFX()
    {
        jumpEffect.SendEvent("OnStartPlay");

        yield return new WaitForSeconds(effectPlayTime);

        jumpEffect.SendEvent("OnStopParticle");
    }
}
