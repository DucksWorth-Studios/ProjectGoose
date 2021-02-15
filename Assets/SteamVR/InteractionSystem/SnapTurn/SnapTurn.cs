// Copyright (c) Valve Corporation, All rights reserved. ======================================================================================================



using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    //-----------------------------------------------------------------------------
    /// <summary>
    /// Script for allowing player snap turning
    /// 
    /// Original Author: Valve
    /// Modified by: Cameron Scholes
    /// </summary>
    //-----------------------------------------------------------------------------
    public class SnapTurn : MonoBehaviour
    {
       public SteamVR_Action_Boolean snapLeftAction = SteamVR_Input.GetBooleanAction("SnapTurnLeft");
        public SteamVR_Action_Boolean snapRightAction = SteamVR_Input.GetBooleanAction("SnapTurnRight");

        public static float teleportLastActiveTime;

        private bool canRotate = true;

        public float canTurnEverySeconds = 0.4f;


        private void Update()
        {
            if (canRotate && snapLeftAction != null && snapRightAction != null && snapLeftAction.activeBinding && snapRightAction.activeBinding)
            {
                //only allow snap turning after a quarter second after the last teleport
                if (Time.time < (teleportLastActiveTime + canTurnEverySeconds))
                    return;

                // Check for input state
                bool leftHandTurnLeft = snapLeftAction.GetStateDown(SteamVR_Input_Sources.LeftHand);
                bool rightHandTurnLeft = snapLeftAction.GetStateDown(SteamVR_Input_Sources.RightHand);

                bool leftHandTurnRight = snapRightAction.GetStateDown(SteamVR_Input_Sources.LeftHand);
                bool rightHandTurnRight = snapRightAction.GetStateDown(SteamVR_Input_Sources.RightHand);

                if (leftHandTurnLeft || rightHandTurnLeft)
                {
                    RotatePlayer(-ComfortManager.settingsData.snapTurnAngle);
                }
                else if (leftHandTurnRight || rightHandTurnRight)
                {
                    RotatePlayer(ComfortManager.settingsData.snapTurnAngle);
                }
            }
        }


        private Coroutine rotateCoroutine;
        public void RotatePlayer(float angle)
        {
            if (rotateCoroutine != null)
            {
                StopCoroutine(rotateCoroutine);
            }

            rotateCoroutine = StartCoroutine(DoRotatePlayer(angle));
        }

        //-----------------------------------------------------
        private IEnumerator DoRotatePlayer(float angle)
        {
            Player player = Player.instance;

            canRotate = false;
            
            CameraBlackout.instance.TriggerSnapTurnBlackout();

            Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
            player.trackingOriginTransform.position -= playerFeetOffset;
            player.transform.Rotate(Vector3.up, angle);
            playerFeetOffset = Quaternion.Euler(0.0f, angle, 0.0f) * playerFeetOffset;
            player.trackingOriginTransform.position += playerFeetOffset;

            float startTime = Time.time;
            float endTime = startTime + canTurnEverySeconds;

            while (Time.time <= endTime)
            {
                yield return null;
            };

            canRotate = true;
        }
    }
}