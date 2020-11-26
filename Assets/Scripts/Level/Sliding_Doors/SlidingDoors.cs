using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that controls the animations of the sliding doors
/// Author: Andrew
/// </summary>
public class SlidingDoors : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Called when the player enters the child collision box
    /// Will play the open doors animation
    /// </summary>
    public void OpenDoors()
    {
        animator.SetBool("Open_Doors", true);
    }

    
}
