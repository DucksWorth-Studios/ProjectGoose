using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public GameObject objectToHold;
    public bool isHolding = true;
    public bool keepHolding = true;
    void Start()
    {
        KeepObjectInZone();
        //Set as not interactable
        objectToHold.GetComponent<Valve.VR.InteractionSystem.Interactable>().enabled = false;
    }
    public void setInteractable()
    {
        //set interactable
        objectToHold.GetComponent<Valve.VR.InteractionSystem.Interactable>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectToHold)
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
        if(objectToHold.transform.position != this.transform.position)
        {
            objectToHold.transform.position = this.transform.position;
        }
        if(objectToHold.transform.rotation != this.transform.rotation)
        {
            objectToHold.transform.rotation = this.transform.rotation;
        }
    }
}
