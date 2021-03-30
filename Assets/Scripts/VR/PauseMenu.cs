using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class PauseMenu : MonoBehaviour
{
    public SteamVR_Action_Boolean pauseAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Pause");
    public GameObject pauseMenu;

    void Start()
    {
        EventManager.instance.OnEnableMovement += EnablePause;
        EventManager.instance.OnDisableMovement += DisablePause;
    }

    void Update()
    {
        if (!enabled)
            return;
        
        string scene = SceneManager.GetActiveScene().name;

        if (!pauseAction.stateDown || scene == "StartScene") 
            return;
        
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);

        if (pauseMenu.activeInHierarchy)
        {
            SettingsManager.instance.Load();
            ComfortManager.instance.Load();
            
            EventManager.instance.PauseGame();
        }
        else
            EventManager.instance.ResumeGame(ComfortManager.instance.GetPointerState());
    }
    
    private void EnablePause()
    {
        enabled = true;
    }

    private void DisablePause()
    {
        enabled = false;
    }
}
