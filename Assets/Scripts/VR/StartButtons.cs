using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Cameron Scholes
/// Script for the buttons on the main menu
/// </summary>

public class StartButtons : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject optionsMenu;
    
    public void StartBtn()
    {
        SceneManager.LoadScene("Loading");
    }

    public void OptionsBtn()
    {
        optionsMenu.SetActive(true);
        startMenu.gameObject.SetActive(false);
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
