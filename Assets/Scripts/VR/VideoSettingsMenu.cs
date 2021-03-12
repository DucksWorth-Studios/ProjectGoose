using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VideoSettingsMenu : MonoBehaviour
{
    [Header("Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fullscreenDropdown;
    public TMP_Dropdown shadowQualityDropdown;
    public TMP_Dropdown anisotropicFilteringDropdown;
    
    [Header("Sliders")]
    public Slider antiAliasingSlider;
    public Slider shadowDistanceSlider;
    
    [Header("Text")]
    public TextMeshProUGUI antiAliasingText;
    public TextMeshProUGUI shadowDistanceText;
    public TextMeshProUGUI saveText;

    [Header("Other Menus")] 
    public GameObject startMenu;
    public GameObject optionsMenu;

    private int currentResolution;
    
    void Start()
    {
        EventManager.instance.OnUpdateVideoSettingsUI += UpdateUI;
        EventManager.instance.OnApplyVideoSettings += Apply;
        
        // Resolution Setup
        resolutionDropdown.AddOptions(GetResolutionsDropdownData());
    }

    private void OnEnable()
    {
        SettingsManager.instance.Load();
    }

    List<TMP_Dropdown.OptionData> GetResolutionsDropdownData()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        Resolution currentRes = Screen.currentResolution;
        SettingsManager.settingsData.width = currentRes.width;
        SettingsManager.settingsData.height = currentRes.height;
        
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            string res = Screen.resolutions[i].ToString();
            options.Add(new TMP_Dropdown.OptionData(res));

            if (currentRes.width == SettingsManager.settingsData.width && 
                currentRes.height == SettingsManager.settingsData.height)
                currentResolution = i;
        }

        // Debug.Log(currentResString);
        return options;
    }
    
    private void UpdateUI()
    {
        UpdateCurrentResolutionIndex();
        resolutionDropdown.SetValueWithoutNotify(currentResolution);

        fullscreenDropdown.SetValueWithoutNotify(SettingsManager.settingsData.fullScreenMode);
        
        shadowQualityDropdown.SetValueWithoutNotify(SettingsManager.settingsData.shadowQuality);
        shadowDistanceSlider.SetValueWithoutNotify(SettingsManager.settingsData.shadowDistance);
        OnChangeShadowDistance(SettingsManager.settingsData.shadowDistance);
        
        anisotropicFilteringDropdown.SetValueWithoutNotify(SettingsManager.settingsData.anisotropicFiltering);
        antiAliasingSlider.SetValueWithoutNotify(SettingsManager.settingsData.antiAliasing);
        OnChangeAntiAliasing(SettingsManager.settingsData.antiAliasing);
        
        gameObject.SetActive(false);
    }

    private void UpdateCurrentResolutionIndex()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            Resolution res = Screen.resolutions[i];

            if (res.width == SettingsManager.settingsData.width && 
                res.height == SettingsManager.settingsData.height)
                currentResolution = i;
        }

        // Debug.Log("Current Resolution: " + Screen.currentResolution);
        // Debug.Log("Settings Data Resolution: " + settingsData.resolution);
        // Debug.Log("Resolution Index: " + currentResolution);
    }
    
    public void Apply()
    {
        // Resolution
        Resolution resolution = Screen.resolutions[resolutionDropdown.value];
        FullScreenMode fsm = (FullScreenMode) fullscreenDropdown.value;

        SettingsManager.settingsData.width = resolution.width;
        SettingsManager.settingsData.height = resolution.height;
        SettingsManager.settingsData.fullScreenMode = fullscreenDropdown.value;
        
        Debug.Log(resolution.ToString());
        Debug.Log(fsm);
        
        Screen.SetResolution(resolution.width, resolution.height, fsm);
        
        // Shadow Quality
        SettingsManager.settingsData.shadowQuality = shadowQualityDropdown.value;
        ShadowQuality sq = (ShadowQuality) shadowQualityDropdown.value;

        Debug.Log(sq);
        QualitySettings.shadows = sq;
        
        // Shadow Distance
        QualitySettings.shadowDistance = SettingsManager.settingsData.shadowDistance;
        
        // Anisotropic Filtering
        SettingsManager.settingsData.anisotropicFiltering = anisotropicFilteringDropdown.value;
        AnisotropicFiltering af = (AnisotropicFiltering) anisotropicFilteringDropdown.value;
        
        Debug.Log(af);
        QualitySettings.anisotropicFiltering = af;
        
        // Anti-Aliasing
        QualitySettings.antiAliasing = SettingsManager.settingsData.antiAliasing;
    }
    
    public void Close()
    {
        optionsMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);
    }

    #region Events
    
    public void OnChangeAntiAliasing(float aaLevel)
    {
        antiAliasingText.text = "" + aaLevel;
        SettingsManager.settingsData.antiAliasing = Mathf.FloorToInt(aaLevel);
    }
    
    public void OnChangeShadowDistance(float shadowDistance)
    {
        shadowDistanceText.text = "" + shadowDistance;
        SettingsManager.settingsData.shadowDistance = Mathf.FloorToInt(shadowDistance);
    }

    private void HideSaveText()
    {
        saveText.gameObject.SetActive(false);
    }
    
    #endregion Events
    
    #region Save/Load
    
    public void Save()
    {
        Apply();

        saveText.gameObject.SetActive(true);
        saveText.text = SettingsManager.instance.Save();
        
        Invoke("HideSaveText", 5);
    }

    #endregion Save/Load

}
