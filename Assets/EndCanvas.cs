using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Cameron Scholes
/// Script for end screen buttons
/// </summary>

public class EndCanvas : MonoBehaviour
{
    private Canvas canvas;
    
    private void Start()
    {
        // Debug.Log("EndCanvas start fired", this);
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = CanvasPointer.eventCamera;
    }

    public void Menu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Quit()
    {
        // Debug.Log("EndCanvas quit fired", this);
        Application.Quit();
    }
}
