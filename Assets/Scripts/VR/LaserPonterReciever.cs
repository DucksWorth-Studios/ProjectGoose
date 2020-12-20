
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LaserPonterReciever : MonoBehaviour
{
    public Color defaultColour = Color.white;
    public Color hitColour = Color.red;
    public Color clickColour = Color.green;

    private MeshRenderer meshRenderer;
    

    // Speed the interactable will move towards the hand
    private float speed = 2;
    private Vector3 handTarget;
    private bool movingTowardsHand;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = defaultColour;
    }

    void Update()
    {
        if (movingTowardsHand)
            MoveTowardsHand();
    }

    public void HitByRay()
    {
        meshRenderer.material.color = hitColour;
    }
    
    public void RayExit()
    {
        // Debug.Log("RayExit: " + name);
        meshRenderer.material.color = defaultColour;
    }

    public void Click(Transform handLocation)
    {
        meshRenderer.material.color = clickColour;
        
        handTarget = handLocation.position;
        movingTowardsHand = true;
    }

    private void MoveTowardsHand()
    {
        // Move our position a step closer to the target.
        float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, handTarget, step);
        
        // Check if the position of the intractable and hand are relatively the same
        if (Vector3.Distance(transform.position, handTarget) < 0.001f)
            movingTowardsHand = false;
    }
}
