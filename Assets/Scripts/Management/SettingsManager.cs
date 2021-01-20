using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manager for loading, saving and applying video settings
/// Author: Cameron Scholes
/// References: https://github.com/Acimaz/open-project-1
///             https://github.com/UnityTechnologies/UniteNow20-Persistent-Data
/// </summary>
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    private static SettingsData settingsData;
    
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fullscreenDropdown;
    public TMP_Dropdown shadowQualityDropdown;
    public TMP_Dropdown anisotropicFilteringDropdown;
    public Button applyBtn;
    public Button saveBtn;
    public Button closeBtn;
    private int currentResolution;
    private string settingsFile = "Settings.dat";

    void Start()
    {
        instance = this;
        
        // Resolution Setup
        resolutionDropdown.AddOptions(GetResolutionsDropdownData());
        resolutionDropdown.SetValueWithoutNotify(currentResolution);
        
        // Fullscreen Mode Setup
        Enum.TryParse(Screen.fullScreenMode.ToString(), out FullScreenMode fsm);
        settingsData.fullScreenMode = (int) fsm;
        
        Debug.Log(Screen.fullScreenMode);
        Debug.Log(settingsData.fullScreenMode);
        fullscreenDropdown.SetValueWithoutNotify(settingsData.fullScreenMode);
        
        Load();
    }

    List<TMP_Dropdown.OptionData> GetResolutionsDropdownData()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        string currentResString = Screen.currentResolution.ToString();
        settingsData.resolution = currentResString;
        
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

    #region Save/Load

    
    [Serializable]
    public struct SettingsData
    {
        public string resolution;
        public int fullScreenMode;
    }
    
    private void Save()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, settingsFile);
        
        try
        {
            File.WriteAllText(fullPath, JsonUtility.ToJson(settingsData));
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
        }
    }

    private void Load()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, settingsFile);
        
        try
        {
            string data = File.ReadAllText(fullPath);
            Debug.Log(data);
            
            JsonUtility.FromJsonOverwrite(data, settingsData);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
        }
    }

    #endregion Save/Load
    
}
