using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FadeCamera : MonoBehaviour
{
    private float _fadeDuration = 0f;
 
    private void Start()
    {
        FadeToWhite();
        Invoke("FadeFromWhite", _fadeDuration);
    }
    private void FadeToWhite()
    {
        Debug.Log("FadeToWhite");
        //set start color
        SteamVR_Fade.View(Color.clear, 0f);
        //set and start fade to
        SteamVR_Fade.View(Color.black, _fadeDuration);
    }
    private void FadeFromWhite()
    {
        Debug.Log("FadeFromWhite");
        //set start color
        SteamVR_Fade.View(Color.black, 0f);
        //set and start fade to
        SteamVR_Fade.View(Color.clear, _fadeDuration);
    }
 
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter: VRCamera");
        FadeToWhite();
    }
 
    public void OnTriggerExit(Collider other)
    {
        FadeFromWhite();
    }
}
