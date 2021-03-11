using System.Collections;
using System.Collections.Generic;
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
        if (isHolding) currentlyHeldObject.transform.position = snapPosition.position;
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

                    currentlyHeldObject.transform.position = snapPosition.position;
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
        currentlyHeldObject.GetComponent<Interactable>().onAttachedToHand -= DetachObject; // unsubscribe from event

        isHolding = false;
        currentlyHeldObject = null;
    }
}
