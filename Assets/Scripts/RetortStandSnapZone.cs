using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RetortStandSnapZone : GenericSnapZone
{
    [Tooltip("Tag To Search For")]
    public string tagToSearchFor;

    [Tooltip("The Roation of The Snapped Object")]
    public Vector3 rotation;

    void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(currentlyHeldObject == null && other.gameObject.tag == tagToSearchFor)
        {
            other.gameObject.GetComponent<Throwable>().ToggleGravity();

            currentlyHeldObject = other.gameObject;

            currentlyHeldObject.GetComponent<Interactable>().onAttachedToHand += DetachObject;
            currentlyHeldObject.transform.position = snapPosition.position;
            currentlyHeldObject.transform.rotation = Quaternion.Euler(rotation);

            isHolding = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isHolding)
        {
            currentlyHeldObject.transform.position = snapPosition.position;
            currentlyHeldObject.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
