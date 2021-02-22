using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Cameron Scholes
/// Manager to make VR player changes on scene cchange
/// </summary>

public class LevelSwitchManager : MonoBehaviour
{
    private string lastScene;
    private VRPlayerDimensionJump dimensionJump;

    void Start()
    {
        dimensionJump = GetComponent<VRPlayerDimensionJump>();
    }
    
    void Update()
    {
        string scene = SceneManager.GetActiveScene().name;
        
        if (scene == lastScene)
            return;
        
        lastScene = scene;
        print(scene);

        if (scene == "StartScene")
            InStart();
        else if (scene == "Loading")
            InLoading();
        else if (scene == "LabCompound")
            InLab();
        else
            InDefault();
    }

    private void InStart()
    {
        EventManager.instance.EnableAllInput(PointerState.CanvasPointer);
        EventManager.instance.DisableJumping();
    }
    
    // Changes to apply when in Loading scene
    private void InLoading()
    {
        EventManager.instance.DisableAllInput();
        transform.position = new Vector3(0, 2, 0);
        transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1f);
    }

    // Changes to apply when in LabCompound scene
    private void InLab()
    {
        transform.position = new Vector3(0.096f, 0.542f, 4.526f);
        transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 1f);
        dimensionJump.teleportPoints = TeleportPoint.points;
        
        EventManager.instance.Progress(STAGE.START);
        EventManager.instance.EnableAllInput(PointerState.PhysicsPointer);
    }

    // Changes to apply when in sandbox or unknown scene
    private void InDefault()
    {
        transform.position = AppData.defaultPosition;
        transform.rotation = AppData.defaultRotation;
        dimensionJump.teleportPoints = TeleportPoint.points;
        
        EventManager.instance.EnableAllInput(PointerState.PhysicsPointer);
    }
}
