using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public float timeOut = 10f;
    public Color fadeColor = Color.black;
    void Start()
    {
        EventManager.instance.OnFadeScreen += FadeEvent;
    }


    private void FadeEvent(bool fadeout)
    {
        if(fadeout)
        {
            print("Ha");
            //Valve.VR.SteamVR_Fade.View(Color.clear, 0);
            Valve.VR.SteamVR_Fade.View(fadeColor,1);
        }
        else
        {
            Valve.VR.SteamVR_Fade.Start(fadeColor, 0);
            Valve.VR.SteamVR_Fade.Start(Color.clear, timeOut);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //print("BOO");
        if(Input.GetKeyDown(KeyCode.O))
        {
            EventManager.instance.Fade(true);
            print("MEH");
        }
    }
}
