using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Broken_Door_Handle : MonoBehaviour
{
    [Tooltip("The local position of the handle so it stays attahced to the door")]
    private Vector3 handlePosition;

    [Tooltip("The local rotation of the handle")]
    private Quaternion handleRotation;

    private Interactable interactable;

    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags
                                                   & ~Hand.AttachmentFlags.SnapOnAttach
                                                   & ~Hand.AttachmentFlags.DetachOthers
                                                   & ~Hand.AttachmentFlags.VelocityMovement;

    private bool canCalculateForce = false;

    // Start is called before the first frame update
    void Start()
    {
        handlePosition = transform.localPosition;
        handleRotation = transform.localRotation;

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

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            hand.HoverLock(interactable);

            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
            canCalculateForce = true;
        }
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);

            hand.HoverUnlock(interactable);

            ResetValues();
        }
    }

    private void ResetValues()
    {
        canCalculateForce = false;

        transform.localPosition = handlePosition;
        transform.localRotation = handleRotation;
    }
}
