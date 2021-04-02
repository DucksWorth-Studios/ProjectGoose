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

    private Quaternion rotation;

    private bool canAttach = true;

    // Update is called once per frame
    void Update()
    {
        if(isHolding)
        {
            //FixPosition();
            currentlyHeldObject.transform.position = snapPosition.position;
            currentlyHeldObject.transform.rotation = rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(canAttach)
        {
            if (other.gameObject.CompareTag(tagToSearchFor))
            {
                try
                {
                    var interactable = other.GetComponent<Interactable>();
                    if (interactable.attachedToHand == null)
                    {
                        currentlyHeldObject = other.gameObject;
                        interactable.onAttachedToHand += DetachObject; // Subscribe to onAttachToHand event so the object can be detached

                        currentlyHeldObject.GetComponent<Rigidbody>().useGravity = false;

                        rotation = currentlyHeldObject.transform.rotation;

                        currentlyHeldObject.transform.position = snapPosition.position;
                        currentlyHeldObject.transform.rotation = rotation;
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
    }

    public IEnumerator AttachObject()
    {
        try
        {
            var interactable = currentlyHeldObject.GetComponent<Interactable>();
            interactable.onAttachedToHand -= DetachObject;

            armController.AttachObjectToArm(currentlyHeldObject.gameObject);

            currentlyHeldObject = null;
            isHolding = false;

            canAttach = false; // Delay attachments to allow object to exit
        }
        catch(System.NullReferenceException e)
        {
            yield break;
        }

        yield return new WaitForSeconds(3f);

        canAttach = true;
    }
}
