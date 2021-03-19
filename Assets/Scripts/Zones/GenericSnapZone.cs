using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Andrew Carolan
/// A generic snap zone that can snap any object to it has long as it has the interactable component
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class GenericSnapZone : MonoBehaviour
{
    [Tooltip("The object being held")]
    protected GameObject currentlyHeldObject;
    protected bool isHolding = false;

    [Tooltip("The position the object shoudl snap to")]
    public Transform snapPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding)
        {
            currentlyHeldObject.transform.position = snapPosition.position;
            currentlyHeldObject.transform.rotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(currentlyHeldObject == null)
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
                }
            }
            catch(System.NullReferenceException e)
            {
                //if exception is thrown it means object cannot snap to zone. SO we ignore it
                return;
            }
        }
    }

    protected void DetachObject(Hand hand)
    {
        var inter = currentlyHeldObject.GetComponent<Interactable>(); // unsubscribe from event
        inter.onAttachedToHand -= DetachObject;
        Debug.Log(inter.attachedToHand);
        currentlyHeldObject.GetComponent<Rigidbody>().useGravity = true; // enable physics

        //isHolding = false;
        currentlyHeldObject = null;
        isHolding = false;       
    }
}
