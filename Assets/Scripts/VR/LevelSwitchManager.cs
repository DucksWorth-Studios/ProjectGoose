using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchManager : MonoBehaviour
{
    private string lastScene;
    
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
        }
    }
}
