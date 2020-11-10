using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    // Single is the name Vector1
    public SteamVR_Action_Single startLaser = SteamVR_Input.GetSingleAction("StartLaser");
    public SteamVR_Action_Boolean pullObject = SteamVR_Input.GetBooleanAction("PullObject");
    
    public float defaultLength = 3.0f;

    private LineRenderer lineRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        if (startLaser == null)
            Debug.LogError("LaserPointer is missing StartLaser.", this);
        
        if (pullObject == null)
            Debug.LogError("LaserPointer is missing PullObject.", this);
        
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLength();
    }

    private void UpdateLength()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, CalculateEnd());
    }
    
    private Vector3 CalculateEnd()
    {
        RaycastHit hit = CreateForwardRaycast();
        Vector3 endPosition = DefaultEnd(defaultLength);

        if (hit.collider)
            endPosition = hit.point;
        
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
}
