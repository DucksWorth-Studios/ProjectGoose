using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Cameron Scholes
/// The Not Russels method of interacting with objects
/// </summary>
public class NotRussels : MonoBehaviour
{
    [Header("Inputs")]
    public SteamVR_Action_Single startLaser = SteamVR_Input.GetSingleAction("StartLaser");
    public SteamVR_Action_Boolean pullObject = SteamVR_Input.GetBooleanAction("PullObject");
    public SteamVR_Action_Pose pose = SteamVR_Input.GetPoseAction("Pose");

    [Header("References")] 
    public Transform objectAttachmentPoint;
    public Transform endTarget;
    
    [Header("Parameters")] 
    
    [Range(0.0f, 0.5f)]
    [Tooltip("The radius to find objects in")]
    public float radius = 0.25f;
    
    [Range(0.0f, 1.0f)]
    [Tooltip("The amount to multiply velocity by")]
    public float velocityDampening = 0.65f; 
    // Current best values
    // 0.65 for palm
    // 0.5 for forward
    
    [Range(0.0f, 1.0f)]
    [Tooltip("The value the player must exceed for the object to move")]
    public float handVelocitySensitivity;

    [Header("Debug")] 
    public bool debug = true;
    public GameObject debugPrefab;
    public TargetingStyle targetingStyle;

    private LaserPonterReciever lastHit;

    private bool materialUpdated;
    private bool isMoving;

    private void Start()
    {
        if (debug)
            SpawnRing();
    }
    
    private void Update()
    {
        CalculateHit();
    }

    private void CalculateHit()
    {
        // The number of returned colliders is limited to this allocated buffer
        Collider[] colliders = new Collider[AppData.BufferAllocation];
        int noOfColliders = Physics.OverlapCapsuleNonAlloc(transform.position, endTarget.transform.position, 
            radius, colliders, AppData.InteractableLayerMask);

        if (noOfColliders > 0)
        {
            Debug.LogWarning("We got " + noOfColliders);
            Collider target = noOfColliders > 1 ? FindClosestTarget(colliders) : colliders[0];
            
            lastHit = target.transform.GetComponent<LaserPonterReciever>();

            if (!lastHit)
                return;

            // Material update should only be called once per object
            if (!materialUpdated)
            {
                lastHit.HitByRay();
                materialUpdated = true;
            }

            if (IsClicking() && !isMoving) 
                ThrowObject(lastHit);
        }
        else
            RayExit();
    }

    private void ThrowObject(LaserPonterReciever lpr)
    {
        // Transform cameraTransform = Camera.main.transform;
        Transform cameraTransform = Player.instance.hmdTransform;
            
        float handVelocity = Vector3.Dot(pose[Player.instance.rightHand.handType].velocity,
            (cameraTransform.forward - cameraTransform.up));

        // Debug.Log("Hand Velocity: " + handVelocity);
            
        if (handVelocity < -handVelocitySensitivity)
        {
            isMoving = true;
                
            //Calculate velocity and apply it to the target
            Vector3 velocity = NotRusselsCalculations.CalculateParabola(lpr.gameObject.transform.position, 
                objectAttachmentPoint.position);
            
            Debug.Log("Velocity: " + velocity);
            Debug.Log("Dampened Velocity: " + velocity * velocityDampening);
            lpr.rigidbody.velocity = velocity * velocityDampening;
        }
    }
    
    private void RayExit()
    {
        if (!lastHit)
            return;

        lastHit.RayExit();
        lastHit = null;
        materialUpdated = false;
        isMoving = false;
    }
    
    // Taken from: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    Collider FindClosestTarget(Collider[] targets)
    {
        Collider closest = targets[0];
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        
        foreach (Collider target in targets)
        {
            if (!target)
                 continue;
            
            Vector3 diff = target.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            
            if (curDistance < distance)
            {
                closest = target;
                distance = curDistance;
            }
        }

        return closest;
    }
    
    // For checking if the player is pulling the trigger down for enough
    // Only the Index supports boolean click being separate to trigger axis
    private bool IsClicking()
    {
        return pullObject.state && startLaser.axis > 0.95f;
    }
    
    private void SpawnRing()
    {
        int spawnLimit = 5;
        float xOffset = 0.5f;
        
        for (int i = 0; i < spawnLimit; i++)
        {
            Vector3 localPosition = endTarget.position;
            Vector3 newPosition = Vector3.zero;
            
            float theta = i * 2 * Mathf.PI / spawnLimit;
            float x = Mathf.Sin(theta) * radius + localPosition.x;
            float y = Mathf.Sin(theta) * radius + localPosition.y;
            float z = Mathf.Cos(theta) * radius + localPosition.z;

            switch (targetingStyle)
            {
                case TargetingStyle.Palm:
                    newPosition = new Vector3(localPosition.x, y, z);
                    break;
                case TargetingStyle.Forward:
                    newPosition = new Vector3(x, localPosition.y, z);
                    break;
            }
                
           
            GameObject ob = Instantiate(debugPrefab, endTarget, true);
            ob.transform.position = newPosition;
            ob.name = "Circle-" + i;
        }
    }

    public enum TargetingStyle
    {
        Palm,
        Forward
    }
}