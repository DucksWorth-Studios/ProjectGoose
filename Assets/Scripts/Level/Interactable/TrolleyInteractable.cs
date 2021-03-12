using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class TrolleyInteractable : MonoBehaviour
{
    private Interactable interactable;

    private Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.VelocityMovement
                                                    & ~Hand.AttachmentFlags.DetachOthers
                                                    & ~Hand.AttachmentFlags.DetachFromOtherHand;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        if(interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            hand.HoverLock(interactable);

            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
        }
        else if(isGrabEnding)
        {
            hand.DetachObject(gameObject);

            hand.HoverUnlock(interactable);
        }
    }
}
