using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Andrew Carolan
/// A snap zone for the robotic arm. This zone will be placed in front of it so that it can grab the 
/// object that is placed in the snap zone
/// </summary>
public class RobotArmSnapZone : GenericSnapZone
{
    public string tagToSearchFor;

    public RoboticArmController armController;

    // Update is called once per frame
    void Update()
    {
        if(isHolding)
        {
            FixPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(tagToSearchFor))
        {
            try
            {
                var interactable = other.GetComponent<Interactable>();
                if (interactable.attachedToHand == null)
                {
                    currentlyHeldObject = other.gameObject;
                    interactable.onAttachedToHand += DetachObject; // Subscribe to onAttachToHand event so the object can be detached

                    currentlyHeldObject.GetComponent<Rigidbody>().useGravity = false;

                    currentlyHeldObject.transform.position = snapPosition.position;
                    currentlyHeldObject.transform.rotation = Quaternion.identity;
                    isHolding = true;

                    armController.PlayGrabAnim();
                }
            }
            catch (System.NullReferenceException e)
            {
                //if exception is thrown it means object cannot snap to zone. SO we ignore it
                return;
            }
        }
    }

    public void AttachObject()
    {
        try
        {
            var interactable = currentlyHeldObject.GetComponent<Interactable>();
            interactable.onAttachedToHand -= DetachObject;

            armController.AttachObjectToArm(currentlyHeldObject.gameObject);

            currentlyHeldObject = null;
            isHolding = false;
        }
        catch(System.NullReferenceException e)
        {
            return;
        }
    }
}
