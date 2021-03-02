using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Cameron Scholes
/// The laser pointer method of interacting with objects
/// </summary>

[RequireComponent(typeof(LineRenderer))]
public class NotRussels : MonoBehaviour
{
    public bool enabled;
    
    public float defaultLength = 3.0f;

    private LineRenderer lineRenderer;
    private LaserPonterReciever lastHit;

    public Hand pointerHand;
    private bool materialUpdated;
    private bool wasClicked;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        lineRenderer.enabled = true;
    }

    void Update()
    {
        UpdateLength();
    }

    private void UpdateLength()
    {
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

            if (lastHit != null)
                if (!materialUpdated)
                {
                    lastHit.HitByRay();
                    // lastHit = null;
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
        Ray ray = new Ray(transform.position, transform.right * -1);

        Physics.Raycast(ray, out hit, defaultLength);
        
        return hit;
    }

    private Vector3 DefaultEnd(float length)
    {
        return transform.position + (transform.right * (-1 * length));
    }

    private void RayExit()
    {
        if (!lastHit) return;
        
        Debug.Log("RayExit");
        
        lastHit.RayExit();
        lastHit = null;
        materialUpdated = false;
        wasClicked = false;
    }
}
