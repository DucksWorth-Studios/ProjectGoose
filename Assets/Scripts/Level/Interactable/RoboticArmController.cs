using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Andrew Carolan
/// A script for controlling the robotic arm. This script will trigger its animations
/// </summary>
[RequireComponent(typeof(Animator))]
public class RoboticArmController : MonoBehaviour
{
    private Animator animator;

    private float nextPlayTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextPlayTime == 0) nextPlayTime = Random.Range(1f, 3.5f);

        if(Time.time > nextPlayTime)
        {
            animator.SetTrigger("SimpleMovement");

            nextPlayTime = Time.time + Random.Range(2.5f, 5f);
        }
    }
}
