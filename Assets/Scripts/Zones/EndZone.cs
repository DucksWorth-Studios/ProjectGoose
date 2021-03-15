using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    [Tooltip("The Rotation object should stay in")]
    public Vector3 rotation;

    //The Composition
    private GameObject solution;
    //string eventToCall might be used to differentiate events
    private bool isSnapped = false;

    private void OnTriggerEnter(Collider other)
    {
        if ("Vial" == other.gameObject.tag)
        {
            if(other.gameObject.GetComponentInChildren<CompositionManager>().HasElement)
            {
                isSnapped = true;
                other.gameObject.GetComponent<Throwable>().attachmentEnabled = false;
                other.gameObject.GetComponent<Valve.VR.InteractionSystem.Interactable>().attachedToHand.DetachObject(other.gameObject, true);
               

                other.gameObject.transform.position = this.transform.position;
                other.gameObject.transform.rotation = Quaternion.Euler(rotation);
                solution = other.gameObject;
                EventManager.instance.Progress(STAGE.END);
                EventManager.instance.WinGame();
            }
        }
    }

    private void Update()
    {
        if (isSnapped)
        {
            solution.transform.position = this.transform.position;
            solution.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
