using UnityEngine;
using Valve.VR;

public class ObjectSwapTest : MonoBehaviour {
    public SteamVR_Action_Boolean uiInteractAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    public SteamVR_Input_Sources hand;
    
    public GameObject normalObject;
    public GameObject upsideDownObject;
    
    public bool upSideDown;

    // Start is called before the first frame update
    private void Start() {
        SetActiveObject();
    }

    // Update is called once per frame
    private void Update() {
        // This reduces if nesting
        if (!uiInteractAction.GetStateDown(hand)) return;

        upSideDown = !upSideDown;
        SetActiveObject();
    }

    private void SetActiveObject() {
        if (!upSideDown) {
            upsideDownObject.SetActive(false);
            normalObject.SetActive(true);
        } else {
            upsideDownObject.SetActive(true);
            normalObject.SetActive(false);
        }
    }
}
