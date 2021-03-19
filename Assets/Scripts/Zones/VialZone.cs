using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Tomas
/// Zone For the Final Composition. Checks if element is in the vial if it is then the composition is correct and allows object to snap
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class VialZone : MonoBehaviour
{
    [Tooltip("The Rotation object should stay in")]
    public Vector3 rotation;

    //The Composition
    private GameObject solution;
    //is object currently Snapped
    public bool isSnapped = false;

    private void OnTriggerEnter(Collider other)
    {
        //Checks If Vial
        if ("Vial" == other.gameObject.tag)
        {
            //Checks If It Has the Element
            if(other.gameObject.GetComponentInChildren<CompositionManager>().HasElement)
            {
                //Diasbales Interactions and Detaches From hand
                isSnapped = true;
                other.gameObject.GetComponent<Throwable>().attachmentEnabled = false;
                other.gameObject.GetComponent<Valve.VR.InteractionSystem.Interactable>().attachedToHand.DetachObject(other.gameObject, true);
               

                other.gameObject.transform.position = this.transform.position;
                other.gameObject.transform.rotation = Quaternion.Euler(rotation);
                solution = other.gameObject;

                //Send Game Over Events
                EventManager.instance.Progress(STAGE.END);
                EventManager.instance.WinGame();
            }
        }
    }

    private void Update()
    {
        //Keeps Items In Zone. Rigidbody ddeeactivation will not disable Gravity
        if (isSnapped)
        {
            solution.transform.position = this.transform.position;
            solution.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
