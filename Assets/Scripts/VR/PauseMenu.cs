using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class PauseMenu : MonoBehaviour
{
    public SteamVR_Action_Boolean pauseAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Pause");
    public GameObject pauseMenu;

    void Update()
    {
        string scene = SceneManager.GetActiveScene().name;

        if (!pauseAction.stateDown || scene == "StartScene") 
            return;
        
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);

        if (pauseMenu.activeInHierarchy)
        {
            EventManager.instance.DisableAllInput();
            EventManager.instance.SetUIPointer();
        }
        else
            EventManager.instance.EnableAllInput();
    }
}
