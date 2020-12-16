using UnityEngine;
using Valve.VR;

/// <summary>
/// Script to fadeout camera view when a collision is detected.
/// Based on:
/// https://forum.unity.com/threads/head-collider-steam-vr-plugin.805743/
/// https://github.com/ValveSoftware/steamvr_unity_plugin/issues/388
/// </summary>
public class FadeCamera : MonoBehaviour
{
    private float _fadeDuration = 0.1f;
 
    private void Start()
    {
        FadeToWhite();
        Invoke("FadeFromWhite", _fadeDuration);
    }
    private void FadeToWhite()
    {
        // Debug.Log("FadeToWhite");
        //set start color
        SteamVR_Fade.View(Color.clear, 0f);
        //set and start fade to
        SteamVR_Fade.View(Color.white, _fadeDuration);
    }
    private void FadeFromWhite()
    {
        // Debug.Log("FadeFromWhite");
        //set start color
        SteamVR_Fade.View(Color.white, 0f);
        //set and start fade to
        SteamVR_Fade.View(Color.clear, _fadeDuration);
    }
 
    public void OnTriggerEnter(Collider other)
    {
        // Debug.Log("TriggerEnter: VRCamera");
        FadeToWhite();
    }
 
    public void OnTriggerExit(Collider other)
    {
        FadeFromWhite();
    }
}
