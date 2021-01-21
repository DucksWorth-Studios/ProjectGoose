using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Andrew Carolan
/// Will be attached to each mesh object to change the material when it is carried to the decayed world
/// </summary>
public class DecayMaterial : MonoBehaviour
{
    [Tooltip("Interactable script in the parent gameobject to check if this object is being held at the time of dimension jump")]
    private Interactable interactable;

    [Tooltip("If true, the materials will change over a set time")]
    private bool isDecaying = false;

    [Tooltip("is the object decayed?")]
    public bool isDecayed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent != null)
            interactable = GetComponentInParent<Interactable>();
        else
            interactable = GetComponent<Interactable>();

        EventManager.instance.OnTimeJump += StartMaterialDecay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartMaterialDecay()
    {
        if (interactable.attachedToHand != null) // Object is in the hand of the player
            isDecayed = true;
    }
}
