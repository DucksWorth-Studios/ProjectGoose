using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Andrew Carolan
/// Will control the sliding doors in the future version of the world. 
/// The player will have to apply a force to each door to make them open
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Interactable))]
public class BrokenSlidingDoors : MonoBehaviour
{
    public Transform StartPosition;
    public Transform EndPosition;

    private Vector3 handStartGrabPos;

    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

    private Rigidbody rigidbody;
    private Interactable interactable;

    private bool isAttached = false;

    [Tooltip("Hand needed for force calculations")]
    private Hand hand;

    // Start is called before the first frame update
    void Start()
    {
        //if (this.transform != StartPosition)
        //    this.transform.position = StartPosition.position;

        rigidbody = GetComponent<Rigidbody>();
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttached)
        {
            ///TODO add code for calculating force
            CalculateForceApplied();
        }
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        if(interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            hand.HoverLock(interactable);

            //hand.AttachObject(gameObject, startingGrabType, attachmentFlags);

            this.hand = hand;
            handStartGrabPos = hand.transform.position;
            isAttached = true;
        }
        else if(isGrabEnding)
        {
            //hand.DetachObject(gameObject);
            isAttached = false;

            hand.HoverUnlock(interactable);
        }
    }

    private void CalculateForceApplied()
    {
        //Vector from start hand point to current hand position
        Vector3 startToHand = hand.transform.position - handStartGrabPos;
        rigidbody.AddForce(Vector3.forward * startToHand.magnitude * 2);;
    }
}
