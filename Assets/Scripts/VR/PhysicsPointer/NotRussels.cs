using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Cameron Scholes
/// The Not Russels method of interacting with objects
/// </summary>
public class NotRussels : MonoBehaviour
{
    public SteamVR_Action_Single startLaser = SteamVR_Input.GetSingleAction("StartLaser");
    public SteamVR_Action_Boolean pullObject = SteamVR_Input.GetBooleanAction("PullObject");
    public SteamVR_Action_Pose pose = SteamVR_Input.GetPoseAction("Pose");
    
    public Transform endTarget;
    public float radius = 0.1f;
    
    [Range(0.0f, 1.0f)]
    [Tooltip("The value the player must exceed for the object to move")]
    public float handVelocitySensitivity;

    public GameObject debugPrefab;

    private LaserPonterReciever lastHit;

    private bool materialUpdated;
    // private bool wasClicked;
    // private bool updated;

    private void Start()
    {
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

            if (!materialUpdated)
            {
                lastHit.HitByRay();
                materialUpdated = true;
            }

            if (!IsClicking()) 
                return;

            float handVelocity = Vector3.Dot(pose[Player.instance.rightHand.handType].velocity,
                (Camera.main.transform.forward - Camera.main.transform.up));

            Debug.Log("Hand Velocity: " + handVelocity);
            
            if (handVelocity < -handVelocitySensitivity)
            {
                //Calculate velocity and apply it to the target
                Vector3 velocity = NotRusselsCalculations.CalculateParabola(lastHit.transform.position, transform.position);
                lastHit.GetComponent<Rigidbody>().velocity = velocity;
            }
        }
        else
        {
            if (!lastHit)
                return;

            lastHit.RayExit();
            lastHit = null;
            materialUpdated = false;
        }
    }

    // Taken from: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    Collider FindClosestTarget(Collider[] targets)
    {
        Collider closest = targets[0];
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        
        foreach (Collider target in targets)
        {
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
            
            float theta = i * 2 * Mathf.PI / spawnLimit;
            float y = Mathf.Sin(theta) * radius + localPosition.y;
            float z = Mathf.Cos(theta) * radius + localPosition.z;
  
            GameObject ob = Instantiate(debugPrefab, endTarget, true);
            Vector3 newPosition = new Vector3(localPosition.x, y, z);
            ob.transform.position = newPosition;
            ob.name = "Circle-" + i;
        }
    }
}