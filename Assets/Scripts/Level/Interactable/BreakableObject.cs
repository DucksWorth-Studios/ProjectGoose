﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Andrew Carolan
/// Component that will be added to objects that can be broken
/// </summary>
[RequireComponent(typeof(Throwable))]
[RequireComponent(typeof(AudioSource))]
public class BreakableObject : MonoBehaviour
{
    [Tooltip("The prefab containing the broken object. This object should have rigidbody and collision components on each mesh in prefab")]
    public GameObject brokenPrefab;

    [Tooltip("The magnitude of the velocity that is needed for the object to break")]
    public float breakingVelocity = 2;

    [Tooltip("Set to true if the broken object has multiple pieces. Will define if a force will be applied to each piece on collision to make the breaking more realistic")]
    public bool isShatterable = false;

    public AudioClip clip;

    /// <summary>
    /// Called when this object collides with another object. If the collision force is large enough the object will spawn the broken object and destroy this one.
    /// </summary>
    /// <param name="collision">The object this object is colliding with</param>
    private void OnCollisionEnter(Collision collision)
    {
        // Stop glass smashing when colliding with player
        if (collision.gameObject.CompareTag("Player"))
            return;
        
        if (collision.relativeVelocity.magnitude < breakingVelocity)
            return;

        var newObject = Instantiate(brokenPrefab, transform.position, transform.rotation);

        if(isShatterable)
        {
            var pieces = newObject.GetComponentsInChildren<Rigidbody>();

            var player = GameObject.FindGameObjectWithTag("Player"); //Need player position to create a direction vector

            Vector3 direction = this.transform.position - player.transform.position;
            direction.y = 0; // Don't care about height
            direction.Normalize(); // Return the direction at which the object was travelling on collision

            foreach(Rigidbody rigidbody in pieces)
            {
                rigidbody.AddForce(direction * (collision.relativeVelocity.magnitude / 2), ForceMode.Impulse);
            }
        }

        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, transform.position);
        else
            Debug.LogError("No Audio CLip Provided");

        Destroy(gameObject);
    }
}
