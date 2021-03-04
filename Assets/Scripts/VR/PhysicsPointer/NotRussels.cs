using UnityEngine;

/// <summary>
/// Author: Cameron Scholes
/// The Not Russels method of interacting with objects
/// </summary>
public class NotRussels : MonoBehaviour
{
    public Transform endTarget;
    public float radius = 0.1f;

    private LaserPonterReciever lastHit;

    private bool materialUpdated;
    // private bool wasClicked;
    // private bool updated;

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
}