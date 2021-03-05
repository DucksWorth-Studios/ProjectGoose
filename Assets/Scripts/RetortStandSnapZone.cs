using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetortStandSnapZone : MonoBehaviour
{
    [Tooltip("Tag To Search For")]
    public string tagToSearchFor;

    [Tooltip("The Roation of The Snapped Object")]
    public Vector3 rotation;

    /// <summary>
    /// Trigger volume needs to be bigger than the snap zone to catch the objects that use gravity
    /// This transform will set the object to the correct position
    /// </summary>
    [Tooltip("The position of the snapped object")]
    private Vector3 position;

    public Transform snappedTransform;

    private GameObject objectCurrentlyHeld;
    private bool isHolding = false;
    void Start()
    {
        position = snappedTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(objectCurrentlyHeld == null && other.gameObject.tag == tagToSearchFor)
        {
            other.gameObject.GetComponent<Throwable>().ToggleGravity();

            objectCurrentlyHeld = other.gameObject;
            objectCurrentlyHeld.transform.position = position;
            objectCurrentlyHeld.transform.rotation = Quaternion.Euler(rotation);

            isHolding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == objectCurrentlyHeld && other.gameObject.GetComponent<Valve.VR.InteractionSystem.Interactable>().attachedToHand != null)
        {
            isHolding = false;
            objectCurrentlyHeld = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isHolding)
        {
            objectCurrentlyHeld.transform.position = snappedTransform.position;
            objectCurrentlyHeld.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
