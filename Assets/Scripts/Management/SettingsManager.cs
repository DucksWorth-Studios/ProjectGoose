using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Author: Cameron Scholes
/// References: https://github.com/Acimaz/open-project-1
/// </summary>
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    
    public TMP_Dropdown resolutionDropdown;
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
        resolutionDropdown.AddOptions(GetResolutionsDropdownData());
    }

    List<TMP_Dropdown.OptionData> GetResolutionsDropdownData()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (Resolution res in Screen.resolutions)
        {
            options.Add(new TMP_Dropdown.OptionData(res.ToString()));
        }

        return options;
    }
}
