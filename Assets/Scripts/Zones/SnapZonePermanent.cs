﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Author: Tomas
/// Snap zones will be used for keys and other objects. When an item enters designated zone it will snap to position triggering an Event.
/// </summary>
public class SnapZonePermanent : MonoBehaviour
{
    [Tooltip("The Object that will Snap here")]
    public GameObject objectToSnap;

    [Tooltip("The Rotation object should stay in")]
    public Vector3 rotation;

    [Tooltip("Event The Object Should Send")]
    public Snap itemSnapped;
    //string eventToCall might be used to differentiate events
    public bool isSnapped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (objectToSnap == other.gameObject && isSnapped == false)
        {
            isSnapped = true;
            
            objectToSnap.GetComponent<Valve.VR.InteractionSystem.Interactable>().attachedToHand.DetachObject(objectToSnap,true);
            objectToSnap.GetComponent<Throwable>().attachmentEnabled = false;

            objectToSnap.transform.position = this.transform.position;
            //Send event
            EventManager.instance.SnappedItem(itemSnapped);
        }
    }

    private void Update()
    {
        if(isSnapped)
        {
            objectToSnap.transform.position = this.transform.position;
            objectToSnap.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}

