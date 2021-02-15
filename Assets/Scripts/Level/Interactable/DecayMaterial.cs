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
    private const float FULL_OPACITY = 1.0f;

    [Tooltip("Interactable script in the parent gameobject to check if this object is being held at the time of dimension jump")]
    private Interactable interactable;

    [Tooltip("If true, the materials will change over a set time")]
    private bool isDecaying = false;

    [Tooltip("is the object decayed?")]
    public bool isDecayed = false;

    [Tooltip("Get the mesh renderer to change the materials of the object")]
    private MeshRenderer renderer;

    #region V1
    //private float timeSinceJump = 0.0f;

    //public Material baseMat;
    //public Material decayedMat;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    if (this.transform.parent != null)
    //        interactable = GetComponentInParent<Interactable>();
    //    else
    //        interactable = GetComponent<Interactable>();

    //    renderer = GetComponent<MeshRenderer>();

    //    if (!isDecayed)
    //        renderer.material = baseMat;
    //    else
    //        renderer.material = decayedMat;

    //    EventManager.instance.OnTimeJump += StartMaterialDecay;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (isDecaying)
    //        DecayMaterials();
    //}

    //private void StartMaterialDecay()
    //{
    //    if (interactable.attachedToHand != null) // Object is in the hand of the player
    //        isDecaying = true;
    //}

    ///// <summary>
    ///// Will wait till half a second after the player has jumped to the decayed world and change the texture of the object to the decayed material
    ///// TODO Add a VFX to make swap cleaner
    ///// </summary>
    //private void DecayMaterials()
    //{
    //    timeSinceJump += 0.5f * Time.deltaTime;

    //    if (timeSinceJump < 0.5f)
    //        return;

    //    if (!isDecayed)
    //    {
    //        renderer.material = decayedMat;
    //    }
    //    else
    //    {
    //        renderer.material = baseMat;
    //    }

    //    if (timeSinceJump > FULL_OPACITY)
    //    {
    //        isDecayed = isDecayed != true ? true : false;
    //        isDecaying = false;
    //        timeSinceJump = 0;
    //    }
    //}
    #endregion

    #region V2
    Shader matShader;

    private void Start()
    {
        if (this.transform.parent != null)
            interactable = GetComponentInParent<Interactable>();
        else
            interactable = GetComponent<Interactable>();

        renderer = GetComponent<MeshRenderer>();

        matShader = renderer.material.shader;
    }

    private void Update()
    {
        if (isDecayed)
            renderer.material.SetFloat("BlendAmount", 1); ///Set the float variable which controls 
    }
    #endregion
}
