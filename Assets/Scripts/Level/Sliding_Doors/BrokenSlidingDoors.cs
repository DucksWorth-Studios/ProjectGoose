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

    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

    private Rigidbody rigidbody;
    private Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        if (this.transform != StartPosition)
            this.transform.position = StartPosition.position;

        rigidbody = GetComponent<Rigidbody>();
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandHoverUpdate(Hand hand)
    {

    }
}
