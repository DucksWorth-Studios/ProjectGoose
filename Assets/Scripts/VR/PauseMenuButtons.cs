using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject pause;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    
    public void Resume()
    {
        pause.SetActive(false);
        EventManager.instance.EnableAllInput();
    }

    public void Options()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
