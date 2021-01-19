using System;
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
    public TMP_Dropdown fullscreenDropdown;
    public TMP_Dropdown shadowQualityDropdown;
    public TMP_Dropdown anisotropicFilteringDropdown;
    private int currentResolution;

    void Start()
    {
        instance = this;
        
        resolutionDropdown.AddOptions(GetResolutionsDropdownData());
        resolutionDropdown.SetValueWithoutNotify(currentResolution);
        
        // Debug.Log(Screen.fullScreenMode);
        // fullscreenDropdown.SetValueWithoutNotify(Screen.fullScreenMode);
        
        shadowQualityDropdown.AddOptions(GetDropdownData(Enum.GetNames(typeof(ShadowQuality))));
        anisotropicFilteringDropdown.AddOptions(GetDropdownData(Enum.GetNames(typeof(AnisotropicFiltering))));
    }

    List<TMP_Dropdown.OptionData> GetResolutionsDropdownData()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        string currentResString = Screen.currentResolution.ToString();
        
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            string res = Screen.resolutions[i].ToString();
            options.Add(new TMP_Dropdown.OptionData(res));

            if (res == currentResString)
                currentResolution = i;
        }

        Debug.Log(currentResString);
        return options;
    }
    
    List<TMP_Dropdown.OptionData> GetDropdownData(string[] optionNames)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        
        foreach (string option in optionNames)
        {
            options.Add(new TMP_Dropdown.OptionData(option));
        }
        
        return options;
    }
    
}
