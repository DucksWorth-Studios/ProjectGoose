using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(BoxCollider))]
public class GenericSnapZone : MonoBehaviour
{
    public GameObject currentlyHeldObject;
    private bool isHolding = false;
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
            if(other.GetComponent<Interactable>().attachedToHand == null)
            {
                currentlyHeldObject = other.gameObject;
                //other.GetComponent<Interactable>().onAttachedToHand += DetachObject;

                currentlyHeldObject.transform.position = snapPosition.position;
                isHolding = true;
            }
        }
    }
}
