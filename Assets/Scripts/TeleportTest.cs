using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TeleportTest : MonoBehaviour
{
    public SteamVR_Action_Boolean uiInteractAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    public SteamVR_Input_Sources hand;
    public GameObject player;
    private bool upSideDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (uiInteractAction.GetStateDown(hand)) {
            if (!upSideDown) {
                player.transform.position += Vector3.up * 100;
                upSideDown = true;
            } else {
                player.transform.position += Vector3.down * 100;
                upSideDown = false;
            }
        }
    }
}
