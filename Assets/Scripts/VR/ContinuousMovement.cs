﻿using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Cameron Scholes
/// Script for taking joystick input to move the VR player
/// </summary>
public class ContinuousMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 2.5f;
    private CharacterController characterController;
    private bool walking = false;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        if (input == null)
            Debug.LogError("ContinuousMovement.cs is missing input", this);
    }

    void FixedUpdate()
    {
        // Prevent locomotion interfering with teleportation
        if (input.axis.magnitude > 0.1f)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
            // First line is movement based on where the headset is. ProjectOnPlane ensures that all movement is horizontal
            // Second line is adding gravity
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) 
                                      - new Vector3(0, 9.81f, 0) * Time.deltaTime);
            startWalk();
        }
        else
        {
            Vector3 direction = Player.instance.hmdTransform.localPosition;
            characterController.center = new Vector3(direction.x, characterController.center.y, direction.z);
            stopWalk();
        }
    }

    private void startWalk()
    {
        if(!walking)
        {
            walking = true;
            EventManager.instance.PlaySound(Sound.Walk);
        }
    }

    private void stopWalk()
    {
        if (walking)
        {
            walking = false;
            EventManager.instance.StopSound(Sound.Walk);
        }
    }
}
