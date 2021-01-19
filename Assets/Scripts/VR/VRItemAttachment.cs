using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Cameron Scholes
/// This will allow interactables to attach to the hand without being switched when dimesnion jumping
/// </summary>

[RequireComponent( typeof( Interactable ) )]
public class VRItemAttachment : MonoBehaviour
{
    [Tooltip("If enabled, the item will teleport back to its original location when detached from from the players hand")]
    public bool teleportBackToOrigin;

    [Tooltip("If false, the player won't be able to pick up the item")]
    public bool attachmentEnabled = true;
    
    private Interactable interactable;
    private bool attached;
    private Vector3 oldPosition;
    private Quaternion oldRotation;
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags 
                                                   & ~Hand.AttachmentFlags.SnapOnAttach
                                                   & ~Hand.AttachmentFlags.DetachOthers
                                                   & ~Hand.AttachmentFlags.VelocityMovement;
    
    // region Properties

    public bool IsAttached
    {
        set => attached = value;
        get => attached;
    }

    // endregion Properties
    
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }    
    
    /// <summary>
    /// Called every Update() while a Hand is hovering over this object
    /// This is the object attachment function
    /// </summary>
    /// <param name="hand">The hand that is currently hovering over the object</param>
    private void HandHoverUpdate( Hand hand )
    {
        if (!attachmentEnabled) return;
        
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            if (teleportBackToOrigin)
            {
                Transform transform1 = transform;
                
                // Save our position/rotation so that we can restore it when we detach
                oldPosition = transform1.position;
                oldRotation = transform1.rotation;
            }

            // Call this to continue receiving HandHoverUpdate messages,
            // and prevent the hand from hovering over anything else
            hand.HoverLock(interactable);

            // Attach this object to the hand
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
            EventManager.instance.PlaySound(Sound.ItemPickUp);
            attached = true;
        }
        else if (isGrabEnding)
        {
            // Detach this object from the hand
            hand.DetachObject(gameObject);
            attached = false;

            // Call this to undo HoverLock
            hand.HoverUnlock(interactable);

            if (teleportBackToOrigin)
            {
                // Restore position/rotation
                transform.position = oldPosition;
                transform.rotation = oldRotation;
            }
        }
    }
}
