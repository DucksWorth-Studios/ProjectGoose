
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LaserPonterReciever : MonoBehaviour
{
    public Color defaultColour = Color.white;
    public Color hitColour = Color.red;
    public Color clickColour = Color.green;

    private MeshRenderer meshRenderer;
    private VRItemAttachment attachScript;
    private Interactable interactable;
    
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags 
                                                   & ~Hand.AttachmentFlags.SnapOnAttach
                                                   & ~Hand.AttachmentFlags.DetachOthers
                                                   & ~Hand.AttachmentFlags.VelocityMovement;

    private Hand hand;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        attachScript = GetComponentInParent<VRItemAttachment>();
        interactable = GetComponentInParent<Interactable>();
        meshRenderer.material.color = defaultColour;
    }

    // void Update()
    // {
    //     if (!attachScript.IsAttached) return;
    //     
    //     if (hand.IsGrabEnding(gameObject))
    //         RayExit();
    // }

    public void HitByRay()
    {
        meshRenderer.material.color = hitColour;
    }
    
    public void RayExit()
    {
        Debug.Log("RayExit: " + name);
        meshRenderer.material.color = defaultColour;
        
        if (!hand) return;
        
        hand.DetachObject(gameObject);
        attachScript.IsAttached = false;
        
        hand.HoverUnlock(interactable);
    }

    public void Click(Transform handLocation, Hand hand)
    {
        this.hand = hand;
        meshRenderer.material.color = clickColour;
        
        hand.HoverLock(interactable);
        
        transform.position = handLocation.position;
        hand.AttachObject(gameObject, GrabTypes.Scripted, attachmentFlags);
        attachScript.IsAttached = true;
    }
}
