using UnityEngine;
using Valve.VR;

/// <summary>
/// Author: Cameron Scholes
/// The laser pointer method of interacting with objects
/// </summary>

[RequireComponent(typeof(LineRenderer))]
public class LaserPointer : MonoBehaviour
{
#if UNITY_INCLUDE_TESTS
#else 
    public SteamVR_Action_Single startLaser = SteamVR_Input.GetSingleAction("StartLaser");
    public SteamVR_Action_Boolean pullObject = SteamVR_Input.GetBooleanAction("PullObject");
#endif
    
    public float defaultLength = 3.0f;

    private LineRenderer lineRenderer;
    private LaserPointerReciever lastHit;

    [HideInInspector] public bool turnOnLaser;
    [HideInInspector] public bool clickOnObject;

    // Start is called before the first frame update
    void Start()
    {
    #if UNITY_INCLUDE_TESTS
        Debug.Log("Test mode");
    #else
        Debug.Log("Normal mode");
        
        if (startLaser == null)
            Debug.LogError("LaserPointer is missing StartLaser.", this);
        
        if (pullObject == null)
            Debug.LogError("LaserPointer is missing PullObject.", this);
    #endif

        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    #if UNITY_INCLUDE_TESTS
        if (turnOnLaser)
    #else
        if (startLaser.axis > 0.25f || turnOnLaser)
    #endif
        {
            lineRenderer.enabled = true;
            UpdateLength();
        }
        else
        {
            lineRenderer.enabled = false;
            RayExit();
        }
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
            lastHit = hit.transform.GetComponent<LaserPointerReciever>(); // TODO: Is there a less expensive method

        #if UNITY_INCLUDE_TESTS
            if (clickOnObject)
        #else
            if (pullObject.state || clickOnObject)
        #endif
                lastHit.Click();
            else
                lastHit.HitByRay();
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
        if (!lastHit) return;
        
        lastHit.RayExit();
        lastHit = null;
    }
}
