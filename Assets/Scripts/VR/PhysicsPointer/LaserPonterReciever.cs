﻿
using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent( typeof( Interactable ) )]
public class LaserPonterReciever : MonoBehaviour
{
    [Tooltip("Add ALL object mesh renders")]
    public MeshRenderer[] meshRenderers;
    
    // TODO: Make private after debug
    public Color[] defaultColours;
    public Color hitColour = Color.red;
    public Color clickColour = Color.green;

    [Tooltip("The amount to offset the object by when attached to hand")]
    public Vector3 offset;
    
    private Hand pointerHand;
    private Throwable throwable;

    // Speed the interactable will move towards the hand
    private float speed = 2;
    private Vector3 handTarget;
    private bool movingTowardsHand;

    void Awake()
    {
        try
        {
            throwable = GetComponent<Throwable>();
        }
        catch(NullReferenceException e)
        {
            Debug.LogWarning("Missing throwable script. " + e.Message);
        }
        
        defaultColours = new Color[meshRenderers.Length];
        
        for (int i = 0; i < meshRenderers.Length; i++)
            defaultColours[i] = meshRenderers[i].material.color;
    }

    void Update()
    {
        if (movingTowardsHand)
            MoveTowardsHand();
    }

    private void UpdateMat(Color newColour)
    {
        foreach (MeshRenderer mr in meshRenderers)
            mr.material.color = newColour;
    }
    
    private void ResetMat()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
            meshRenderers[i].material.color = defaultColours[i];
    }
    
    public void HitByRay()
    {
        Debug.Log("HitByRay");
        UpdateMat(hitColour);
    }
    
    public void RayExit()
    {
        ResetMat();
        
        // Detach object from hand
        if (pointerHand)
            pointerHand.DetachObject(gameObject);
        
        if (throwable)
            throwable.Detach();
    }

    // public void Click(Transform handLocation)
    public void Click(Hand pointerHand)
    {
        UpdateMat(clickColour);
        
        handTarget = pointerHand.gameObject.transform.position;
        // movingTowardsHand = true;
        TeleportToHand(pointerHand);
    }

    private void MoveTowardsHand()
    {
        // Move our position a step closer to the target.
        float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, handTarget, step);
        
        // Check if the position of the intractable and hand are relatively the same
        if (Vector3.Distance(transform.position, handTarget) < 0.001f)
            movingTowardsHand = false;
    }

    private void TeleportToHand(Hand pointerHand)
    {
        // Store default attachment flags
        Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags 
                              & ~Hand.AttachmentFlags.SnapOnAttach
                              & ~Hand.AttachmentFlags.DetachOthers
                              & ~Hand.AttachmentFlags.VelocityMovement;

        // Move object before attach otherwise the player remote controls the object
        transform.position = pointerHand.gameObject.transform.position + offset;
        
        // Attach object and store hand for future reference
        pointerHand.AttachObject(gameObject, GrabTypes.Grip, attachmentFlags);
        this.pointerHand = pointerHand;
        
        if (throwable)
            throwable.Attach();

        // Reset material colour otherwise object stays green
        ResetMat();
    }
}
