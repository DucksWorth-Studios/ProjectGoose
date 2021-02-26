
using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent( typeof( Interactable ) )]
[RequireComponent( typeof( Outline ) )]
public class LaserPonterReciever : MonoBehaviour
{
    [Tooltip("The amount to offset the object by when attached to hand")]
    public Vector3 offset;
    
    private Hand pointerHand;
    private Throwable throwable;

    // private Vector3 handTarget;
    // private bool movingTowardsHand;
    
    // Outline settings
    private Outline outline;
    private Color outlineColor = new Color(1, 0.5f, 0);
    private float outlineWidth = 2;

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
        
        outline = GetComponent<Outline>();
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineWidth;
        outline.OutlineMode = Outline.Mode.OutlineHidden;
    }

    private void UpdateMat()
    {
        outline.OutlineMode = Outline.Mode.OutlineVisible;
    }
    
    private void ResetMat()
    {
        outline.OutlineMode = Outline.Mode.OutlineHidden;
    }
    
    public void HitByRay()
    {
        // Debug.Log("HitByRay");
        UpdateMat();
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
        UpdateMat();
        
        // handTarget = pointerHand.gameObject.transform.position;
        // movingTowardsHand = true;
        TeleportToHand(pointerHand);
    }

    // private void MoveTowardsHand()
    // {
    //     // Move our position a step closer to the target.
    //     float step =  speed * Time.deltaTime; // calculate distance to move
    //     transform.position = Vector3.MoveTowards(transform.position, handTarget, step);
    //     
    //     // Check if the position of the intractable and hand are relatively the same
    //     if (Vector3.Distance(transform.position, handTarget) < 0.001f)
    //         movingTowardsHand = false;
    // }

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
