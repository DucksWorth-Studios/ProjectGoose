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
    public Slider antiAliasingSlider;
    public Slider shadowDistanceSlider;
    public TextMeshProUGUI antiAliasingText;
    public TextMeshProUGUI shadowDistanceText;
    public TextMeshProUGUI saveText;
    
    private int currentResolution;
    private string settingsFile = "Settings.dat";

    void Start()
    {
        instance = this;
        settingsData = new SettingsData();
        
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

    private void UpdateUI()
    {
        UpdateCurrentResolutionIndex();
        resolutionDropdown.SetValueWithoutNotify(currentResolution);

        fullscreenDropdown.SetValueWithoutNotify(settingsData.fullScreenMode);
        shadowQualityDropdown.SetValueWithoutNotify(settingsData.shadowQuality);
        shadowDistanceSlider.SetValueWithoutNotify(settingsData.shadowDistance);
        anisotropicFilteringDropdown.SetValueWithoutNotify(settingsData.anisotropicFiltering);
        antiAliasingSlider.SetValueWithoutNotify(settingsData.antiAliasing);
    }

    private void UpdateCurrentResolutionIndex()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            string res = Screen.resolutions[i].ToString();

            if (res == settingsData.resolution)
                currentResolution = i;
        }
    }

    public void Apply()
    {
        // Resolution
        Resolution resolution = Screen.resolutions[resolutionDropdown.value];
        FullScreenMode fsm = (FullScreenMode) fullscreenDropdown.value;

        settingsData.resolution = resolution.ToString();
        settingsData.fullScreenMode = fullscreenDropdown.value;
        
        Debug.Log(resolution.ToString());
        Debug.Log(fsm);
        
        Screen.SetResolution(resolution.width, resolution.height, fsm);
        
        // Shadow Quality
        settingsData.shadowQuality = shadowQualityDropdown.value;
        ShadowQuality sq = (ShadowQuality) shadowQualityDropdown.value;

        Debug.Log(sq);
        QualitySettings.shadows = sq;
        
        // Shadow Distance
        QualitySettings.shadowDistance = settingsData.shadowDistance;
        
        // Anisotropic Filtering
        settingsData.anisotropicFiltering = anisotropicFilteringDropdown.value;
        AnisotropicFiltering af = (AnisotropicFiltering) anisotropicFilteringDropdown.value;
        
        Debug.Log(af);
        QualitySettings.anisotropicFiltering = af;
        
        // Anti-Aliasing
        QualitySettings.antiAliasing = settingsData.antiAliasing;
    }

    public void Close()
    {
        gameObject.SetActive(false);
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

    #region Events
    
    public void OnChangeAntiAliasing(float aaLevel)
    {
        antiAliasingText.text = "" + aaLevel;
        settingsData.antiAliasing = Mathf.FloorToInt(aaLevel);
    }
    
    public void OnChangeShadowDistance(float shadowDistance)
    {
        shadowDistanceText.text = "" + shadowDistance;
        settingsData.shadowDistance = Mathf.FloorToInt(shadowDistance);
    }

    private void HideSaveText()
    {
        saveText.gameObject.SetActive(false);
    }
    
    #endregion Events
    
    #region Save/Load

    [Serializable]
    public struct SettingsData
    {
        public string resolution;
        public int fullScreenMode;
        public int shadowQuality;
        public int shadowDistance;
        public int anisotropicFiltering;
        public int antiAliasing;
    }
    
    public void Save()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, settingsFile);
        
        try
        {
            File.WriteAllText(fullPath, JsonUtility.ToJson(settingsData));

            saveText.gameObject.SetActive(true);
            saveText.text = "Saved";
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            
            saveText.gameObject.SetActive(true);
            saveText.text = $"Failed to write to {fullPath} with exception {e}";
        }
        
        Invoke("HideSaveText", 5);
    }

    private void Load()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, settingsFile);
        
        try
        {
            string data = File.ReadAllText(fullPath);
            Debug.Log(data);
            
            JsonUtility.FromJsonOverwrite(data, settingsData);
            UpdateUI();
            UpdateUI();
            Apply();
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
        }
    }

    #endregion Save/Load
    
}
