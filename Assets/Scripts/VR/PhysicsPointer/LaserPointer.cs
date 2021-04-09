using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Cameron Scholes
/// The laser pointer method of interacting with objects
/// </summary>

[RequireComponent(typeof(Hand))]
[RequireComponent(typeof(LineRenderer))]
public class LaserPointer : MonoBehaviour
{
    // Single is the name Vector1
    public SteamVR_Action_Single startLaser = SteamVR_Input.GetSingleAction("StartLaser");
    public SteamVR_Action_Boolean pullObject = SteamVR_Input.GetBooleanAction("PullObject");
    
    public float defaultLength = 3.0f;

    private LineRenderer lineRenderer;
    private LaserPonterReciever lastHit;
    private LaserPonterReciever oldLastHit;

    private Hand pointerHand;
    private bool materialUpdated;
    private bool wasClicked;

    void Awake()
    {
        if (startLaser == null)
            Debug.LogError("LaserPointer is missing StartLaser.", this);
        
        if (pullObject == null)
            Debug.LogError("LaserPointer is missing PullObject.", this);

        lineRenderer = GetComponent<LineRenderer>();
        pointerHand = GetComponent<Hand>();
    }

    void Update()
    {
        if (!enabled)
            return;
        
        if (startLaser.axis > 0.25f && !pointerHand.objectIsAttached && !wasClicked)
        {
            lineRenderer.enabled = true;
            UpdateLength();
        }
        else
        {
            lineRenderer.enabled = false;
            // RayExit();
        }
        
        // if (!IsClicking() && wasClicked)
        //     RayExit();
    }

    private void UpdateLength()
    {
        // Calculate the start of the line render
        lineRenderer.SetPosition(0, transform.position);
        
        // Calculate the end of the line render
        lineRenderer.SetPosition(1, CalculateEnd());
    }
    
    private Vector3 CalculateEnd()
    {
        RaycastHit hit = CreateForwardRaycast();
        Vector3 endPosition;

        if (hit.collider)
        {
            endPosition = hit.point;
            lastHit = hit.transform.GetComponent<LaserPonterReciever>(); // TODO: Is there a less expensive method

            if (!oldLastHit)
                oldLastHit = lastHit;
            else if (oldLastHit != lastHit)
                RayChanged();
            
            if (!lastHit)
            {
                RayExit();
                return endPosition;
            }

            if (IsClicking())
            {
                lastHit.Click(pointerHand);
                wasClicked = true;
            }
            else if (!materialUpdated)
            {
                lastHit.HitByRay();
                materialUpdated = true;
            }
        }
        else
        {
            endPosition = DefaultEnd(defaultLength);
            RayExit();
        }
        
        return endPosition;
    }

    private RaycastHit CreateForwardRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        Physics.Raycast(ray, out hit, defaultLength);
        
        return hit;
    }

    private Vector3 DefaultEnd(float length)
    {
        return transform.position + (transform.forward * length);
    }

    private void RayExit()
    {
        if (!lastHit) 
            return;
        
        // Debug.Log("RayExit");
        
        lastHit.RayExit();
        lastHit = null;
        
        oldLastHit.ResetMat();
        oldLastHit = null;
        
        materialUpdated = false;
        // wasClicked = false;
    }
    
    private void RayChanged()
    {
        if (!oldLastHit) 
            return;
        
        oldLastHit.ResetMat();
        oldLastHit = lastHit;
        materialUpdated = false;
    }

    // For checking if the player is pulling the trigger down for enough
    // Only the Index supports boolean click being separate to trigger axis
    private bool IsClicking()
    {
        return pullObject.state && startLaser.axis > 0.95f;
    }
}
