using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Snap { USB };
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
    private bool isSnapped = false;

    private void OnTriggerEnter(Collider other)
    {
        print("InZone");
        if (objectToSnap == other.gameObject)
        {
            isSnapped = true;
            
            objectToSnap.GetComponent<Valve.VR.InteractionSystem.Interactable>().attachedToHand.DetachObject(objectToSnap,true);
            objectToSnap.GetComponent<VRItemAttachment>().attachmentEnabled = false;


            

            objectToSnap.transform.position = this.transform.position;
            print("HALLLELUJAH");
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

