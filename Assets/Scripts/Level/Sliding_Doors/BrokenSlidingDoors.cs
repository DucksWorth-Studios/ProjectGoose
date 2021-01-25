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

    private Vector3 direction;

    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        direction = EndPosition.position - StartPosition.position;
        direction.Normalize();

        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    /// <summary>
    /// Applies a force and a counter force based on the input from the player
    /// </summary>
    /// <param name="force">The force vector the player is applying to the door</param>
    public void ApplyForce(Vector3 force)
    {
        if(force == Vector3.zero)
        {
            if (rigidBody.velocity == Vector3.zero)
                return;

            //rigidBody.velocity = -rigidBody.velocity;

            rigidBody.AddForce(-rigidBody.velocity);
        }

        //Apply force in the direction the door is sliding
        rigidBody.AddForce(new Vector3(force.x * direction.x, force.y* direction.y, force.z * direction.z));

        //Apply a counter force to provide resistance
        rigidBody.AddForce(new Vector3(force.x * -direction.x/2, force.y* -direction.y/2, force.z * -direction.z/2));
    }
}
