using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Andrew Carolan
/// Will control the sliding doors in the future version of the world. 
/// The player will have to apply a force to each door to make them open
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class BrokenSlidingDoors : MonoBehaviour
{
    public Transform StartPosition;
    public Transform EndPosition;

    private Vector3 handStartGrabPos;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }   
}
