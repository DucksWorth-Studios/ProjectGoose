using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author Tomas
/// The Script Holds Certain items Designated By their Tag
/// </summary>
public class SnapZoneTemporary : MonoBehaviour
{
    [Tooltip("Tag To Search For")]
    public string tagToSearchFor;

    [Tooltip("The Roation of The Snapped Object")]
    public Vector3 rotation;

    //Object Held
    private GameObject objectCurrentlyHeld;
    private bool isHolding = false;
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        if(objectCurrentlyHeld == null && other.gameObject.tag == tagToSearchFor)
        {
            Valve.VR.InteractionSystem.Interactable interactable = other.gameObject.GetComponent< Valve.VR.InteractionSystem.Interactable> ();
            if(interactable.attachedToHand == null)
            {
                objectCurrentlyHeld = other.gameObject;
                objectCurrentlyHeld.transform.position = this.transform.position;
                objectCurrentlyHeld.transform.rotation = Quaternion.Euler(rotation);
            }
        }
        else if (objectCurrentlyHeld == other.gameObject && other.gameObject.transform.rotation != Quaternion.Euler(rotation))
        {
            Valve.VR.InteractionSystem.Interactable interactable = other.gameObject.GetComponent<Valve.VR.InteractionSystem.Interactable>();
            if (interactable.attachedToHand == null)
            {
                objectCurrentlyHeld.transform.rotation = Quaternion.Euler(rotation);
            }
        }
        else if (objectCurrentlyHeld == other.gameObject && other.gameObject.transform.position != this.transform.position)
        {
            Valve.VR.InteractionSystem.Interactable interactable = other.gameObject.GetComponent<Valve.VR.InteractionSystem.Interactable>();
            if (interactable.attachedToHand == null)
            {
                objectCurrentlyHeld.transform.position = this.transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == objectCurrentlyHeld)
        {
            isHolding = false;
            objectCurrentlyHeld = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
