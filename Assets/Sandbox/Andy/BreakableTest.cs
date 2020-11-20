using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTest : MonoBehaviour
{
    public GameObject destroyedObject;

    [Tooltip("This is the magnitude of the relative velocity that is needed to break this object")]
    public float BreakingVelocity = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > BreakingVelocity)
        {
            Instantiate(destroyedObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
