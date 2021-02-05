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

    [Tooltip("The points the player will be randomly sent to when the teleport to the past")]
    public GameObject[] teleportPoints;
    
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
        
        if (teleportPoints.Length == 0)
            Debug.LogError("VRPlayerDimensionJump is missing teleport point(s)", this);
    }
    
    void Update()
    {
        if (dimensionJumpAction.stateDown)
            StartCoroutine(DimensionJump());
    }

    // Move the player up or down depending on the dimension they are in
    public IEnumerator DimensionJump()
    {
        EventManager.instance.TimeJumpButton();

        yield return new WaitForSeconds(0.5f);

        EventManager.instance.TimeJump();
        EventManager.instance.PlaySound(Sound.Teleport);
        if (!upSideDown) {
            // Teleport to the past
            originalLocation = transform.position;
            transform.position = PickRandomPointToJumpTo();
            upSideDown = true;
        } else {
            // Teleport to the future
            transform.position = originalLocation;
            upSideDown = false;
        }
    }

    private Vector3 PickRandomPointToJumpTo()
    {
        GameObject point = teleportPoints[Random.Range(0, teleportPoints.Length)];
        
        Debug.Log("Point: " + point);
        Debug.Log("Point Transform: " + point.transform.position);
        
        return point.transform.position;
    }
}
