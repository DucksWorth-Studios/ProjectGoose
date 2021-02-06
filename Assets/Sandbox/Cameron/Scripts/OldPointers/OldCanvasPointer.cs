using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Cameron Scholes
/// The laser pointer method of interacting with objects
/// </summary>

[RequireComponent(typeof(LineRenderer))]
public class OldCanvasPointer : MonoBehaviour
{
    // Single is the name Vector1
    public SteamVR_Action_Single startLaser = SteamVR_Input.GetSingleAction("StartLaser");
    public SteamVR_Action_Boolean pullObject = SteamVR_Input.GetBooleanAction("PullObject");
    
    public float defaultLength = 3.0f;

    public EventSystem eventSystem;
    public BaseInputModule inputModule;

    private LineRenderer lineRenderer;

    void Awake()
    {
        if (startLaser == null)
            Debug.LogError("LaserPointer is missing StartLaser.", this);
        
        if (pullObject == null)
            Debug.LogError("LaserPointer is missing PullObject.", this);

        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (startLaser.axis > 0.25f)
        {
            lineRenderer.enabled = true;
            UpdateLength();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void UpdateLength()
    {
        // Calculate the start of the line render
        lineRenderer.SetPosition(0, transform.position);
        
        // Calculate the end of the line render
        lineRenderer.SetPosition(1, GetEnd());
    }

    private Vector3 GetEnd()
    {
        float distance = GetCanvasDistance();
        Vector3 endPosition = CalculateEnd(defaultLength);

        if (distance != 0)
            endPosition = CalculateEnd(distance);
        
        return endPosition;
    }
    
    private float GetCanvasDistance()
    {
        //Get Data
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = inputModule.inputOverride.mousePosition;

        //Raycast using data
        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, results);

        //Get Closest
        RaycastResult closest = FindFirstRaycast(results);
        float distance = closest.distance;

        //Clamp
        distance = Mathf.Clamp(distance, 0, defaultLength);
        return distance;
    }

    private RaycastResult FindFirstRaycast(List<RaycastResult> results)
    {
        foreach (RaycastResult result in results)
        {
            if (!result.gameObject)
                continue;

            return result;
        }

        return new RaycastResult();
    }

    private Vector3 CalculateEnd(float length)
    {
        return transform.position + (transform.forward * length);
    }
}
