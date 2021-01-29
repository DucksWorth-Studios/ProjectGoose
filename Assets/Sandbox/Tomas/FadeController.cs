using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public float timeOut = 5f;
    public Color fadeColor = Color.black;
    void Start()
    {
        EventManager.instance.OnFadeScreen += FadeEvent;
        //Valve.VR.SteamVR_Fade.View(Color.black, 0);
    }


    private void FadeEvent(bool fadeout)
    {
        if(fadeout)
        {
            Valve.VR.SteamVR_Fade.View(Color.clear, 0);
            Valve.VR.SteamVR_Fade.View(fadeColor,timeOut);
        }
        else
        {
            Valve.VR.SteamVR_Fade.View(fadeColor, timeOut);
            Valve.VR.SteamVR_Fade.View(Color.clear, timeOut);
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
