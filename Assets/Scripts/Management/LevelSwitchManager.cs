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

        if (scene == "Loading")
        {
            transform.position = new Vector3(0, 2, 0);
            transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1f);
        } else if (scene == "LabCompound")
        {
            transform.position = new Vector3(0.096f, 0.542f, 4.526f);
            transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 1f);
            dimensionJump.teleportPoints = TeleportPoint.points;
        }
    }
}
