using System.Collections;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Author: Cameron Scholes
/// Used to take in controller input to move the player between dimensions
/// </summary>
public class VRPlayerDimensionJump : MonoBehaviour
{
    public SteamVR_Action_Boolean dimensionJumpAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("DimensionJump");

    [Tooltip("Set this to the height difference between the two planes")]
    public float planeDifference = 1.0f;

    private bool isEnabled = true;
    private bool upSideDown;
    private Vector3 originalLocation;

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

        EventManager.instance.OnEnableJumping += EnableJumping;
        EventManager.instance.OnDisableJumping += DisableJumping;
    }

    void Update()
    {
        if (!isEnabled)
            return;
        
        if (dimensionJumpAction.stateDown)
        {
            EventManager.instance.DisableAllInput();
            StartCoroutine(DimensionJump());
        }
    }

    // Move the player up or down depending on the dimension they are in
    public IEnumerator DimensionJump()
    {
        EventManager.instance.TimeJumpButton();

        yield return new WaitForSeconds(0.5f);

        EventManager.instance.TimeJump();
        EventManager.instance.PlaySound(Sound.Teleport);
        if (!upSideDown) {
            // Teleport to the future
            transform.position += new Vector3(0, planeDifference, 0);
            upSideDown = true;
        } else {
            // Teleport to the past
            // Debug.Log("Teleport back to last position", this);
            transform.position  -= new Vector3(0, planeDifference, 0);
            upSideDown = false;
        }
        
        EventManager.instance.EnableAllInput();
    }
    
    private void EnableJumping()
    {
        isEnabled = true;
    }
    
    private void DisableJumping()
    {
        isEnabled = false;
    }
}
