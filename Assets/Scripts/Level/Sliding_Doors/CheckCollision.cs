using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple Script that checks to see if the player has entered/left the collision box.
/// Will activate the sliding doors based on the collision
/// Author: Andrew
/// </summary>
public class CheckCollision : MonoBehaviour
{
    /// <summary>
    /// Checks if any other collider has entered this collider
    /// If the collider is a player object, the open doors method in the parent script will be called
    /// </summary>
    /// <param name="collision">The collider object that enters this collider</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            transform.GetComponentInParent<SlidingDoors>().OpenDoors();
    }

    /// <summary>
    /// Checks if any other collider has exited this collider
    /// If the collider is a player object, the close doors method in the parent script will be called</summary>
    /// <param name="collision">The collider object that exits this collider</param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            transform.GetComponentInParent<SlidingDoors>().CloseDoors();
    }
}
