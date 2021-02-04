using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DimensionJumpEffects : MonoBehaviour
{
    private Color fadeColor = Color.black;
    private float fadeDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnTimeJumpButtonPressed += StartCameraFade;
        EventManager.instance.OnTimeJump += StartVFX;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void StartVFX()
    {

    }
}
