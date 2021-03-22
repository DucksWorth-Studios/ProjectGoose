using UnityEngine;
using Valve.VR;

/// <summary>
/// Author: Cameron Scholes
/// Script to fadeout camera view when a collision is detected.
/// References: https://forum.unity.com/threads/head-collider-steam-vr-plugin.805743/
///             https://github.com/ValveSoftware/steamvr_unity_plugin/issues/388
/// </summary>
public class FadeCamera : MonoBehaviour
{
    public Texture texture;
    private float fadeDuration = 0.1f;
    private Color fadeColor = Color.white;
 
    private void Start()
    {
        FadeToWhite();
        Invoke("FadeFromWhite", fadeDuration);
    }
    private void FadeToWhite()
    {
        // Debug.Log("FadeToWhite");
        //set start color
        SteamVR_Fade.View(Color.clear, 0f);
        //set and start fade to
        SteamVR_Fade.View(fadeColor, fadeDuration);
    }
    private void FadeFromWhite()
    {
        // Debug.Log("FadeFromWhite");
        //set start color
        SteamVR_Fade.View(fadeColor, 0f);
        //set and start fade to
        SteamVR_Fade.View(Color.clear, fadeDuration);
    }
 
    public void OnTriggerEnter(Collider other)
    {
        // ulong handle = OpenVR.k_ulOverlayHandleInvalid;
        // var overlay = OpenVR.Overlay;
        //
        // var tex = new Texture_t();
        // tex.handle = texture.GetNativeTexturePtr();
        // tex.eType = SteamVR.instance.textureType;
        // tex.eColorSpace = EColorSpace.Auto;
        // overlay.SetOverlayTexture(handle, ref tex);
        
        
        // Debug.Log("TriggerEnter: VRCamera");
        if (!(other.CompareTag("PostProcessing") || other.CompareTag("Door")))
            FadeToWhite();
    }
 
    public void OnTriggerExit(Collider other)
    {
        FadeFromWhite();
    }
}
