﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Snap zones will be used for keys and other objects. When an item enters designated zone it will snap to position triggering an Event.
/// </summary>
public class SnapZonePermenant : MonoBehaviour
{
    [Tooltip("The Object that will Snap here")]
    public GameObject objectToSnap;

    [Tooltip("The Rotation object should stay in")]
    public Vector3 rotation;
    //string eventToCall might be used to differentiate events
    private bool isSnapped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (objectToSnap == other.gameObject)
        {
            isSnapped = true;
            objectToSnap.transform.rotation = Quaternion.Euler(rotation);
            objectToSnap.GetComponent<VRItemAttachment>().attachmentEnabled = false;
            objectToSnap.transform.position = this.transform.position;

            //Send event

        }
    }
}

