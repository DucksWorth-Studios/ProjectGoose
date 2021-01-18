using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    
    public string[] resolutions;
    public string currentResolution;
    private bool fullscreen;

    public bool Fullscreen
    {
        get => fullscreen;
        set
        {
            fullscreen = value;
            Screen.fullScreen = value;
            Debug.Log("Fullscreen: " + value);
        }
    }

    void Start()
    {
        instance = this;
        currentResolution = Screen.currentResolution.ToString();
        fullscreen = Screen.fullScreen;
        PopulateResolutionsList();
    }

    private void PopulateResolutionsList()
    {
        Resolution[] ress = Screen.resolutions;
        resolutions = new string[ress.Length];
        
        for(int i = 0; i < ress.Length; i++)
        {
            resolutions[i] = ress[i].ToString();
        }

        Debug.Log(resolutions);
    }
}
