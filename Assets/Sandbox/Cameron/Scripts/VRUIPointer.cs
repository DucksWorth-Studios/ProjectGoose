using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIPointer : MonoBehaviour
{
    public Transform raycastTransform;

    /// <summary>
    /// Current depth of pointer from camera
    /// </summary>
    private float depth = 3;
    
    void Update()
    {
        // Move the gaze cursor to keep it in the middle of the view
        transform.position = raycastTransform.position + raycastTransform.forward * depth;
    }
}
