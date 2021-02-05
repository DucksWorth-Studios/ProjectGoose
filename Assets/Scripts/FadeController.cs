using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Used To Control Fade Outs And Fade Ins
/// </summary>
public class FadeController : MonoBehaviour
{
    public float timeOut = 10f;
    public Color fadeColor = Color.black;
    private void Awake()
    {
       // Valve.VR.SteamVR_Fade.View(Color.black, 0);
    }
    void Start()
    {
        EventManager.instance.OnFadeScreen += FadeEvent;
    }

    /// <summary>
    /// The Main event. the bool passed toggles wether its fadeout event or not if not a fade in event occurs.
    /// </summary>
    /// <param name="fadeout"></param>
    private void FadeEvent(bool fadeout)
    {
        if(fadeout)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeOut()
    {
        Valve.VR.SteamVR_Fade.View(Color.clear, 0);
        Valve.VR.SteamVR_Fade.View(fadeColor, timeOut);
        yield return new WaitForSeconds(1);
    }

    IEnumerator FadeIn()
    {

        Valve.VR.SteamVR_Fade.View(fadeColor, 0);
        yield return new WaitForSeconds(2);
        Valve.VR.SteamVR_Fade.View(Color.clear, timeOut);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
