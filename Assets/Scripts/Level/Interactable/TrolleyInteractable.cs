using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class TrolleyInteractable : MonoBehaviour
{
    private Interactable interactable;

    private Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.VelocityMovement
                                                    & ~Hand.AttachmentFlags.SnapOnAttach
                                                    & ~Hand.AttachmentFlags.DetachOthers
                                                    & ~Hand.AttachmentFlags.DetachFromOtherHand;

    private Hand attachedHand;
    private float previousRot;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(attachedHand != null)
        {
            if(previousRot == 0)
            {
                previousRot = attachedHand.transform.eulerAngles.y;
                return;
            }

            var angle = Vector3.Angle(new Vector3(0, previousRot, 0), new Vector3(0, previousRot = attachedHand.transform.eulerAngles.y, 0));

            this.transform.Rotate(0, angle, 0);

            ///Force Rotation only to move in the y axis
            rigidbody.angularVelocity = Vector3.zero;
            this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
        }
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        if(interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            hand.HoverLock(interactable);

            attachedHand = hand;

            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
        }
        else if(isGrabEnding)
        {
            hand.DetachObject(gameObject);

            attachedHand = null;
            previousRot = 0;

            hand.HoverUnlock(interactable);
        }
    }
}
