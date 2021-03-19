using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Andrew Carolan
/// Controls the physics of the trolley object when the player attaches to it.
/// It is an interactable object that the player can push around and snap smaller objects to
/// </summary>
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

    public GameObject[] wheels;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWheelRotation();

        ApplyReverseForce();
        StopXZRotation();
    }

    /// <summary>
    /// Changes the rotation of the wheels of the trolley based on the direction the trolley is moving
    /// </summary>
    private void ChangeWheelRotation()
    {
        if (wheels == null || wheels.Length == 0) return;

        if (rigidbody.velocity.magnitude > 0.2f)
        {
            foreach (var wheel in wheels)
            {
                wheel.transform.localRotation = Quaternion.LookRotation(-rigidbody.velocity.normalized);

                //wheel.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                wheel.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
            }
        }
    }

    /// <summary>
    /// Will apply a force in the opposite direction the trolley is moving in.
    /// Will be used when the player is not pushing the cart around the level to 
    /// stop it
    /// </summary>
    private void ApplyReverseForce()
    {
        rigidbody.AddForce(rigidbody.velocity * -0.5f);

        if (rigidbody.velocity.magnitude < 0.05f)
            rigidbody.velocity = Vector3.zero;
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

    /// <summary>
    /// Changes the roation of the trolley based off the movements of the hands
    /// </summary>
    private void ChangeTrolleyRotation()
    {
        if (previousRot == 0)
        {
            previousRot = attachedHand.transform.eulerAngles.y;
            return;
        }

        var angle = Vector3.Angle(new Vector3(0, previousRot, 0), new Vector3(0, previousRot = attachedHand.transform.eulerAngles.y, 0));

        this.transform.Rotate(0, angle, 0);

        ///Force Rotation only to move in the y axis
        StopXZRotation();
    }

    /// <summary>
    /// Locks the rotation of the trolley to just the y axis
    /// </summary>
    private void StopXZRotation()
    {
        rigidbody.angularVelocity = Vector3.zero;
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
    }
}
