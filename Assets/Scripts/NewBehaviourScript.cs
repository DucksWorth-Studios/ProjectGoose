using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Valve.VR.InteractionSystem.Interactable interactable;
    private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RightHand")
        {
            print("YEAAAH");
            this.transform.parent = null;
        }
    }
    private void DebugWin()
    {
        print("Winner Winner Chicken Dinner");
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable.attachedToHand != null)
        {
            print("WOOOOOOP");
        }
    }
}
