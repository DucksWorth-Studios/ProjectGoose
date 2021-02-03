using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapZoneHolder : MonoBehaviour
{
    public string tagToSearchFor;
    private GameObject objectCurrentlyHeld;
    public Vector3 rotation;
    private bool isHolding = false;
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        print("Hello"+other.gameObject.tag);
        //print("SearchFor" + tagToSearchFor);
        if(objectCurrentlyHeld == null && other.gameObject.tag == tagToSearchFor)
        {
            print("Set");
            Valve.VR.InteractionSystem.Interactable interactable = other.gameObject.GetComponent< Valve.VR.InteractionSystem.Interactable> ();
            if(interactable.attachedToHand == null)
            {
                print("Hello World");
                objectCurrentlyHeld = other.gameObject;
                objectCurrentlyHeld.transform.position = this.transform.position;
                objectCurrentlyHeld.transform.rotation = Quaternion.Euler(rotation);
            }
        }
        else if (objectCurrentlyHeld == other.gameObject && other.gameObject.transform.rotation != Quaternion.Euler(rotation))
        {
            print("Hello My Child");
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
