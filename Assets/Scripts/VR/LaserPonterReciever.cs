
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LaserPonterReciever : MonoBehaviour
{
    public Color defaultColour = Color.white;
    public Color hitColour = Color.red;
    public Color clickColour = Color.green;

    private MeshRenderer meshRenderer;
    private VRItemAttachment attachScript;
    
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags 
                                                   & ~Hand.AttachmentFlags.SnapOnAttach
                                                   & ~Hand.AttachmentFlags.DetachOthers
                                                   & ~Hand.AttachmentFlags.VelocityMovement;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        attachScript = GetComponentInParent<VRItemAttachment>();
        meshRenderer.material.color = defaultColour;
    }

    public void HitByRay()
    {
        meshRenderer.material.color = hitColour;
    }
    
    public void RayExit()
    {
        meshRenderer.material.color = defaultColour;
    }

    public void Click(Transform handLocation, Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        meshRenderer.material.color = clickColour;
        
        transform.position = handLocation.position;
        hand.AttachObject(gameObject, startingGrabType, attachmentFlags);

        RayExit();
    }
}
