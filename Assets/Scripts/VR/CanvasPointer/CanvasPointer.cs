using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

/// <summary>
/// Author: Cameron Scholes & VR With Andrew
///
/// Script to update physical pointer
/// 
/// Resource: https://www.youtube.com/watch?v=vNqHRD4sqPc
/// </summary>

public class CanvasPointer : MonoBehaviour
{
    public static Camera eventCamera;
    
    public float defaultLength = 5;
    public GameObject dot;
    public VRCanvasInput inputModule;
    public SteamVR_Action_Single startLaser = SteamVR_Input.GetSingleAction("StartLaser");
    
    private LineRenderer lineRenderer;
    
    private void Awake() 
    {
        lineRenderer = GetComponent<LineRenderer>();
        eventCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (startLaser.axis > 0.25f)
        {
            lineRenderer.enabled = true;
            dot.gameObject.SetActive(true);
            UpdateLine();
        }
        else
        {
            lineRenderer.enabled = false;
            dot.gameObject.SetActive(false);
        }
    }

    private void UpdateLine()
    {
        // Use default or distance
        PointerEventData data = inputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;

        // Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        // Default end position
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        // or based on hit
        if (hit.collider != null)
            endPosition = hit.point;

        // Set position of dot
        dot.transform.position = endPosition;

        // Set positions of line render
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, length);
        
        return hit;
    }
}
