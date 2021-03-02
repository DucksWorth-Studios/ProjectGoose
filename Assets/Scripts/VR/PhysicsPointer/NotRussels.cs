using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Cameron Scholes
/// The Not Russels method of interacting with objects
/// </summary>

[RequireComponent(typeof(CapsuleCollider))]
public class NotRussels : MonoBehaviour
{
    public float length = 1.5f;
    private LaserPonterReciever lastHit;
    private CapsuleCollider trigger;

    private bool materialUpdated;
    private bool wasClicked;
    private bool updated;

    private void Awake()
    {
        trigger = GetComponent<CapsuleCollider>();
        CalculateCollider();
    }
    
    // There is only one value so OnValidate replaces Update
    private void OnValidate()
    {
        CalculateCollider();
    }
    
    private void OnTriggerEnter(Collider hit)
    {
        Debug.Log("OnTriggerEnter");
        lastHit = hit.transform.GetComponent<LaserPonterReciever>(); // TODO: Is there a less expensive method

        if (!lastHit) 
            return;
        
        if (!materialUpdated)
        {
            lastHit.HitByRay();
            materialUpdated = true;
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (!lastHit) return;
        
        lastHit.RayExit();
        lastHit = null;
        materialUpdated = false;
        wasClicked = false;
    }

    private void CalculateCollider()
    {
        if (!trigger) return;
        
        trigger.height = length;
        trigger.center = new Vector3(length / -2, 0, 0);
    }
}
