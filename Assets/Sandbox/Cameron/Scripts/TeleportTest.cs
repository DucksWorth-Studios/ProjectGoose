using UnityEngine;
using Valve.VR;

public class TeleportTest : MonoBehaviour {
    public SteamVR_Action_Boolean uiInteractAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("DimensionJump");
    public SteamVR_Input_Sources hand;
    public GameObject player;
    
    private bool upSideDown;

    void Start()
    {
        if (uiInteractAction == null)
            Debug.LogError("TeleportTest is missing uiInteractAction.", this);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!uiInteractAction.GetStateDown(hand)) return;

        if (!upSideDown) {
            player.transform.position += Vector3.up * 100;
            upSideDown = true;
        } else {
            player.transform.position += Vector3.down * 100;
            upSideDown = false;
        }
    }
}
