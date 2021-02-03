using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasPointer : MonoBehaviour
{
    public float defaultLength = 5;
    public GameObject dot;
    public VRCanvasInput inputModule;
    
    private LineRenderer lineRenderer;
    
    private void Awake() 
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // Use default or distance
        PointerEventData data = inputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;

        // Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        // Default
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
        Physics.Raycast(ray, out hit, defaultLength);
        
        return hit;
    }
}
