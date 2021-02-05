using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Cameron Scholes
/// Script for the buttons on the main menu
/// </summary>

public class StartButtons : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Loading");
    }

    public void OptionsBtn()
    {
        throw new System.NotImplementedException();
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
