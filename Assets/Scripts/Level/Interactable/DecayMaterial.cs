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
    private bool isDecayed = false;

    [Tooltip("The first material of the mesh renderer")]
    Material material;

    float blendAmount = 0;
    float timeElasped;
    float duration = 1;

    private void Start()
    {
        if (this.transform.parent != null)
            interactable = GetComponentInParent<Interactable>();
        else
            interactable = GetComponent<Interactable>();

        material = GetComponent<MeshRenderer>().material;

        EventManager.instance.OnTimeJump += StartMaterialDecay;
    }

    private void Update()
    {
        if (isDecaying)
        {
            if(timeElasped < duration)
            {
                /**
                 * Lerps the Blend Amount between 0 and 1 if the object is not decayed, and 1 and 0 if the object is decayed
                 **/
                if (isDecayed)
                    blendAmount = Mathf.Lerp(AppData.Opaque, AppData.Transparent, timeElasped / duration); ///https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/
                else
                    blendAmount = Mathf.Lerp(AppData.Transparent, AppData.Opaque, timeElasped / duration); ///https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/

                timeElasped += Time.deltaTime;

                material.SetFloat("BlendAmount", blendAmount);
            }
            else
            {
                isDecayed = (blendAmount >= 0.95f) ? true : false;
                isDecaying = false;

                timeElasped = 0;
            }
        }
    }

    private void StartMaterialDecay()
    {
        if (interactable.attachedToHand != null) // Object is in the hand of the player
            isDecaying = true;
    }
}
