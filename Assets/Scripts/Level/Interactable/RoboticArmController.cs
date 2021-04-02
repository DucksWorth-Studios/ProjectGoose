using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Andrew Carolan
/// A script for controlling the robotic arm. This script will trigger its animations
/// </summary>
[RequireComponent(typeof(Animator))]
public class RoboticArmController : MonoBehaviour
{
    private Animator animator;

    private float nextPlayTime;

    public GameObject startSnapZone;
    public GameObject armSnapZone;
    private GameObject currentlyHeldObject;
    private bool isHolding = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlaySimpleMovement();

        if (isHolding)
            FixObjectPosition();
    }

    /// <summary>
    /// Changes the held objects position to match the snap zone
    /// </summary>
    private void FixObjectPosition()
    {
        currentlyHeldObject.transform.position = armSnapZone.transform.position;
        currentlyHeldObject.transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Attachs an object to the claw of the arm
    /// </summary>
    /// <param name="objectToAttach">The game object that should be attached to the arm</param>
    public void AttachObjectToArm(GameObject objectToAttach)
    {
        var interactable = objectToAttach.GetComponent<Interactable>();
        interactable.onAttachedToHand += DetachObject;
        currentlyHeldObject = objectToAttach;

        currentlyHeldObject.transform.position = armSnapZone.transform.position;
        currentlyHeldObject.transform.rotation = Quaternion.identity;
        isHolding = true;
    }  

    /// <summary>
    /// Animation Event function. Is called when the arm is at the point where the test tube can be joined
    /// </summary>
    public void AttachObject()
    {
        startSnapZone.GetComponent<RobotArmSnapZone>().AttachObject();
    }

    /// <summary>
    /// A function that plays the simple movement animation on the robotic arm.
    /// Checks if the correct amount of timme has past since it was last played, then plays the animation.
    /// It then uses a random number to choose the next time the animation should play
    /// </summary>
    private void PlaySimpleMovement()
    {
        if (nextPlayTime == 0) nextPlayTime = Random.Range(1f, 3.5f);

        if (Time.time > nextPlayTime)
        {
            animator.SetTrigger("SimpleMovement");

            nextPlayTime = Time.time + Random.Range(2.5f, 5f);
        }
    }

    public void PlayGrabAnim()
    {
        animator.SetTrigger("ComplexMovement");
    }

    /// <summary>
    /// Event function that is called when the object that is being held by the arm is grabbed by the player.
    /// Detaches the object from the arm and re-enables it's gravity
    /// </summary>
    /// <param name="hand"></param>
    private void DetachObject(Hand hand)
    {
        var inter = currentlyHeldObject.GetComponent<Interactable>(); // unsubscribe from event
        inter.onAttachedToHand -= DetachObject;
        currentlyHeldObject.GetComponent<Rigidbody>().useGravity = true; // enable physics

        currentlyHeldObject = null;
        isHolding = false;
    }
}
