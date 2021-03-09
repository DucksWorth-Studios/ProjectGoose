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
    private Color fadeColor = Color.white;
    private float fadeDuration = 0.1f;
    private bool isCameraFading = false;

    //VFX variables
    [Tooltip("VFX that is attach to the watch on the player")]
    public VisualEffect watchEffect;
    [Tooltip("VFX that creates a portal around the player")]
    public VisualEffect portalEffect;

    private bool isEffectPlaying = false;
    private float timeElasped = 0;
    private float intensity = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Event Subscription
        EventManager.instance.OnTimeJumpButtonPressed += CallVFX;
        //EventManager.instance.OnTimeJump += CallVFX;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEffectPlaying)
        {
            UpdateVFX();
        }
        else
        {
            ResetVFXVariables();
        }
    }

    /// <summary>
    /// Updates the variables in the vfx graph if the VFX is currently playing
    /// </summary>
    private void UpdateVFX()
    {
        intensity = Mathf.Lerp(AppData.Transparent, AppData.Opaque, timeElasped / AppData.jumpDelay);

        watchEffect.SetFloat("Intensity", intensity);
        portalEffect.SetFloat("Intensity", intensity);

        timeElasped += Time.deltaTime;

        if (timeElasped > 1.45f && !isCameraFading) StartCoroutine(StartCameraFade());

        if (timeElasped > AppData.jumpDelay) isEffectPlaying = false;
    }

    /// <summary>
    /// Resets the VFX variables for the next tim,e it has to be played
    /// </summary>
    private void ResetVFXVariables()
    {
        watchEffect.SendEvent("OnStopParticle");
        portalEffect.SendEvent("OnStopParticle");
        timeElasped = 0;
        intensity = 0;
        isEffectPlaying = false;
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
        yield return new WaitForSeconds(0.35f);

        //fade back to clear
        SteamVR_Fade.View(Color.clear, fadeDuration);
    }

    /// <summary>
    /// Calls the IEnumerator function StartVFX
    /// </summary>
    private void CallVFX()
    {
        watchEffect.SendEvent("OnStartParticle");
        portalEffect.SendEvent("OnStartParticle");
        isEffectPlaying = true;
    }
}
