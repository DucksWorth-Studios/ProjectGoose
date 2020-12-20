using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class OverlayTest : MonoBehaviour
{
    public Texture texture;
    static public string key { get { return "unity:" + Application.companyName + "." + Application.productName; } }
    
    void Start()
    {
        ulong handle = OpenVR.k_ulOverlayHandleInvalid;
        var overlay = OpenVR.Overlay;
        overlay.CreateOverlay("step-back", gameObject.name, ref handle);
        
        var tex = new Texture_t();
        tex.handle = texture.GetNativeTexturePtr();
        tex.eType = SteamVR.instance.textureType;
        tex.eColorSpace = EColorSpace.Auto;
        overlay.SetOverlayTexture(handle, ref tex);
        
        overlay.SetOverlayWidthInMeters(handle, 3.0f);
        overlay.ShowOverlay(handle);
        
        // overlay.CreateOverlay("overlaykey", "overlayname", ref handle);
        // overlay.SetOverlayRenderModel(handle, "vr_controller_vive_1_5");
        // overlay.SetOverlayTexture(handle, ref tex);
        // overlay.ShowOverlay(handle);
    }
    
}
