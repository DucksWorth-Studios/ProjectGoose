using UnityEngine;
using Valve.VR;

/// <summary>
/// Author: Cameron Scholes
/// Used to take in controller input to move the player between dimensions
/// </summary>
public class VRPlayerDimensionJump : MonoBehaviour
{
    public SteamVR_Action_Boolean dimensionJumpAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("DimensionJump");
    
    private bool upSideDown;

    // region Properties
    
    public bool IsInUpSideDown
    {
        get => upSideDown;
    }
    
    // endregion Properties
    
    void Start()
    {
        // Ensure Steam VR action has being assigned
        if (dimensionJumpAction == null)
            Debug.LogError("VRPlayerDimensionJump is missing dimensionJumpAction.", this);
    }
    
    void Update()
    {
        if (dimensionJumpAction.stateDown)
            DimensionJump();
    }

    // Move the player up or down depending on the dimension they are in
    public void DimensionJump()
    {
        if (!upSideDown) {
            transform.position += Vector3.up * 100;
            upSideDown = true;
        } else {
            transform.position += Vector3.down * 100;
            upSideDown = false;
        }
    }
}
