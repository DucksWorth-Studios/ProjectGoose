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
    public GameObject objectToHold;
    public bool isHolding = true;
    public bool keepHolding = true;
    void Start()
    {
        //Set to SnapZones
        KeepObjectInZone();
        //Set as not interactable not currently working
        objectToHold.GetComponent<VRItemAttachment>().attachmentEnabled = false;
    }

    //Allow Pickup
    public void setInteractable()
    {
        //set interactable
        objectToHold.GetComponent<VRItemAttachment>().attachmentEnabled = true;
        keepHolding = false;
    }
    //Unset object from zone if meets criteria
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectToHold && keepHolding == false)
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
