using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Cameron Scholes
/// This will switch between two objects depending on whether the player is in the future or the past
/// </summary>

[RequireComponent( typeof( VRItemAttachment ) )]
public class VRItemDimensionJump : MonoBehaviour
{
    [Tooltip("The item to be rendered in the present")]
    public GameObject normalObject;
    
    [Tooltip("The desired rigidbody mass of the normal object")]
    public float normalObjectMass = 1;
    
    [Tooltip("The iterm to be rendered in the past")]
    public GameObject agedObject;
    
    [Tooltip("The desired rigidbody mass of the aged object")]
    public float agedObjectMass = 1;

    [Tooltip("Enable if the item starts in the past")]
    public bool inFuture;

    [HideInInspector]
    public VRItemAttachment attachScript;
    
    void Start()
    {
        attachScript = GetComponent<VRItemAttachment>();
        EventManager.instance.OnTimeJump += TimeJump;
        SetActiveObject();
    }

    /// <summary>
    /// OnTimeJump event listener
    /// Only changes object state if the object is being held
    /// </summary>
    private void TimeJump()
    {
        // If the current object isn't attached to a hand, return, not currently being moved
        if (attachScript != null && !attachScript.IsAttached) return;
        
        inFuture = !inFuture;
        SetActiveObject();
    }

    /// <summary>
    /// Swaps the active object depending on the given state
    /// </summary>
    private void SetActiveObject() {
        if (!inFuture) {
            agedObject.SetActive(false);
            normalObject.SetActive(true);
        } else {
            agedObject.SetActive(true);
            normalObject.SetActive(false);
        }
    }
}
