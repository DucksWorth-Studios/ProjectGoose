using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Andrew Carolan
/// Component that will be added to objects that can be broken
/// </summary>
[RequireComponent(typeof(Throwable))]
public class BreakableObject : MonoBehaviour
{
    [Tooltip("The prefab containing the broken object. This object should have rigidbody and collision components on each mesh in prefab")]
    public GameObject brokenPrefab;

    [Tooltip("The magnitude of the velocity that is needed for the object to break")]
    public float breakingVelocity = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Called when this object collides with another object. If the collision force is large enough the object will spawn the broken object and destroy this one.
    /// </summary>
    /// <param name="collision">The object this object is colliding with</param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > breakingVelocity)
        {
            Instantiate(brokenPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
