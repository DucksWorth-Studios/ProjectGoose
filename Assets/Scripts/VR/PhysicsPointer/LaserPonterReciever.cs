using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent( typeof( Interactable ) )]
public class LaserPonterReciever : MonoBehaviour
{
    [Tooltip("The amount to offset the object by when attached to hand")]
    public Vector3 offset;

    public bool outlineDoesntWork;
    public Material backupMaterial;
    
    private Hand pointerHand;
    private Throwable throwable;

    // private Vector3 handTarget;
    // private bool movingTowardsHand;
    
    // Outline settings
    private Outline outline;
    private Color outlineColor = new Color(1, 0.5f, 0);
    private float outlineWidth = 2;
    
    // Backup Outline setup
    private Renderer[] renderers;
    private List<Material> materials;

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
        
        if (!outlineDoesntWork)
        {
            if (TryGetComponent(out Outline outline))
            {
                this.outline = outline;
                outline.OutlineColor = outlineColor;
                outline.OutlineWidth = outlineWidth;
                outline.OutlineMode = Outline.Mode.OutlineHidden;
            }
            else
                Debug.LogError(gameObject.name + " is missing Outline script or outlineDoesntWork flag");
        }
        else
        {
            // Cache renderers
            renderers = GetComponentsInChildren<Renderer>();
            materials = new List<Material>();
            
            foreach (Renderer renderer in renderers) 
            {
                // Get all existing materials to use for resetting later
                List<Material> mats = renderer.sharedMaterials.ToList();
                materials.AddRange(mats);
            }
        }
    }

    private void UpdateMat()
    {
        if (!outlineDoesntWork)
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        else
        {
            foreach (Renderer renderer in renderers) 
            {
                // Get all existing materials
                List<Material> mats = renderer.sharedMaterials.ToList();

                // Change all materials to backup
                for (int i = 0; i < mats.Count; i++)
                    mats[i] = backupMaterial;
                    
                // Push material changes to renderer
                renderer.materials = mats.ToArray();
            }
        }
    }
    
    private void ResetMat()
    {
        if (!outlineDoesntWork)
            outline.OutlineMode = Outline.Mode.OutlineHidden;
        else
        {
            int count = 0;
            
            foreach (Renderer renderer in renderers) 
            {
                // Get all existing materials
                List<Material> mats = renderer.sharedMaterials.ToList();

                // Change all materials back to their original
                for (int i = 0; i < mats.Count; i++)
                {
                    mats[i] = materials[count];
                    count++;
                }
                    
                // Push material changes to renderer
                renderer.materials = mats.ToArray();
            }
        }
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
