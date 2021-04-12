using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent( typeof( Interactable ) )]
[RequireComponent( typeof( Throwable ) )]
[RequireComponent( typeof( Rigidbody ) )]
[RequireComponent( typeof( Outline ) )]
public class LaserPonterReciever : MonoBehaviour
{
    [Tooltip("The amount to offset the object by when attached to hand")]
    public Vector3 offset;

    // References to be used in other scripts
    [HideInInspector] public Throwable throwable;
    [HideInInspector] public Rigidbody rigidbody;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public bool moveToTarget;
    [HideInInspector] public bool interrupt;
    
    private Hand pointerHand;
    
    // Outline settings
    private Outline outline;
    private Color outlineColor = new Color(1, 0.5f, 0);
    private float outlineWidth = 2;
    
    // Backup Outline setup
    private Renderer[] renderers;
    private List<Material> materials;

    void Awake()
    {
        target = gameObject.transform.position;
        GetThrowable();
        GetRigidbody();
        SetupOutline();
    }

    void Start()
    {
        EventManager.instance.OnRATSInterrupt += Interrupt;
    }
    
    void Update()
    {
        if (interrupt)
            return;
        
        if (!moveToTarget)
            return;
        
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            EventManager.instance.RATSNextStep();
            return;
        }

        // Debug.LogWarning("Moving to: " + target);
        
        float step =  AppData.RATSMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    private void Interrupt()
    {
        if (interrupt)
            return;
        
        if (!moveToTarget)
            return;
        
        // Debug.LogWarning("LPR Interrupt", this);
        interrupt = true;
    }
    
    #region Awake Functions

    private void GetThrowable()
    {
        try
        {
            throwable = GetComponent<Throwable>();
            throwable.awakeEnableGravity = true;
            throwable.detatchEnableGravity = true;
        }
        catch(NullReferenceException e)
        {
            Debug.LogWarning("Missing throwable script. " + gameObject.name);
        }
    }
    
    private void GetRigidbody()
    {
        try
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
        }
        catch(NullReferenceException e)
        {
            Debug.LogWarning("Missing rigidbody. " + gameObject.name);
        }
    }

    private void SetupOutline()
    {
        if (TryGetComponent(out Outline outlineRef))
        {
            outline = outlineRef;
            outline.OutlineColor = outlineColor;
            outline.OutlineMode = Outline.Mode.Disabled;
            
            if (outline.OutlineWidth < outlineWidth)
                outline.OutlineWidth = outlineWidth;
        }
        else
            Debug.LogError(gameObject.name + " is missing Outline script or outlineDoesntWork flag");
    }

    private void SetupMaterialReplacement()
    {
        // Cache renderers
        renderers = GetComponentsInChildren<Renderer>();
        materials = new List<Material>();
            
        foreach (Renderer ren in renderers) 
        {
            // Get all existing materials to use for resetting later
            List<Material> mats = ren.sharedMaterials.ToList();
            materials.AddRange(mats);
        }
    }

    #endregion

    private void UpdateMat()
    {
        outline.OutlineColor = outlineColor;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
    }
    
    public void ResetMat()
    {
        if (!outline.blockDisabled)
            outline.OutlineMode = Outline.Mode.Disabled;
        else
            outline.OutlineColor = outline.colourToRevertTo;
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
