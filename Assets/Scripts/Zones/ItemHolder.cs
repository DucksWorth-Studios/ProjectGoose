using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author:Tomas
/// Keeps Items in a zone until further notice
/// </summary>
public class ItemHolder : MonoBehaviour
{
    [Tooltip("Object Item must Hold")]
    public GameObject objectToHold;
    [Tooltip("Is It Holding")]
    public bool isHolding = true;
    [Tooltip("Should it Continue to hold")]
    public bool keepHolding = true;
    void Start()
    {
        //Set to SnapZones
        KeepObjectInZone();
        //Set as not interactable not currently working
        LockInteractable();
    }
    public void LockInteractable()
    {
        print("lock");
        objectToHold.GetComponent<VRItemAttachment>().attachmentEnabled = false;
        keepHolding = true;
    }
    //Allow Pickup
    public void SetInteractable()
    {
        print("SET0");
        //set interactable
        objectToHold.GetComponent<VRItemAttachment>().attachmentEnabled = true;
        keepHolding = false;
    }
    //Unset object from zone if meets criteria
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectToHold && keepHolding == false && objectToHold.GetComponent<Valve.VR.InteractionSystem.Interactable>().attachedToHand != null)
        {
            isHolding = false;
            objectToHold = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        KeepObjectInZone();
    }
    //Ensures Object stays in zone.
    private void KeepObjectInZone()
    {
        if(objectToHold != null)
        {
            if (objectToHold.transform.position != this.transform.position && objectToHold.GetComponent< Valve.VR.InteractionSystem.Interactable>().attachedToHand ==null)
            {
                objectToHold.transform.position = this.transform.position;
            }
            if (objectToHold.transform.rotation != this.transform.rotation && objectToHold.GetComponent<Valve.VR.InteractionSystem.Interactable>().attachedToHand == null)
            {
                objectToHold.transform.rotation = this.transform.rotation;
            }
        }
    }
}
