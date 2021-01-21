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

    private Rigidbody rigidbody;
    private Interactable interactable;

    [Tooltip("Is set when the players hand is hovering on the door, so that a force can be applied")]
    private bool isAttached = false;
    [Tooltip("Used to allow a force to be applied. False when the door has reached its start/end point")]
    private bool canApplyForce = true;

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

        if(rigidbody.velocity.magnitude != 0)
        {
            //Counter Movement - Fricition
            rigidbody.AddForce(Vector3.forward * -rigidbody.velocity.magnitude * 5f);
        }

        if (this.transform.localPosition.x <= EndPosition.localPosition.x)
        {
            ///Stops the door sliding past the end point
            rigidbody.velocity = Vector3.zero;
            canApplyForce = false;
        }
    }

    /// <summary>
    /// Called when the players hand is hovering over the door
    /// sets variables for the code to calculate the force being applied
    /// </summary>
    /// <param name="hand"></param>
    private void OnHandHoverBegin(Hand hand)
    {
        this.hand = hand;
        handStartGrabPos = hand.transform.position;
        isAttached = true;
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if(interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            hand.HoverLock(interactable);
        }
        else
        {
            hand.HoverUnlock(interactable);
        }
    }

    private void OnHandHoverEnd(Hand hand)
    {
        isAttached = false;
    }

    /// <summary>
    /// Calaculates a the force the player is applying to the door
    /// </summary>
    private void CalculateForceApplied()
    {
        if (!canApplyForce)
            return;

        //Vector from start hand point to current hand position
        Vector3 startToHand = hand.transform.position - handStartGrabPos;
        rigidbody.AddForce(Vector3.forward * startToHand.magnitude * 2);
    }
}
