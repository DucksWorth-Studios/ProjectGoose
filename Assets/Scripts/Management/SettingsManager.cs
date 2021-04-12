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
    public static SettingsData settingsData;

    private int currentResolution;
    private string settingsFile = "Settings.dat";

    void Start()
    {
        instance = this;
        settingsData = new SettingsData();
        
        Load();
        // SetDefaults();
    }

    private void SetDefaults()
    {
        // Debug.Log("Before Default: " + Screen.currentResolution);
        Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        // Debug.Log("Width: " + Screen.width);
        // Debug.Log("Height: " + Screen.height);
        // Debug.Log("After Default: " + Screen.currentResolution);
        QualitySettings.shadows = ShadowQuality.Disable;
        QualitySettings.shadowDistance = 0;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        QualitySettings.antiAliasing = 0;

        UpdateSettings();
        Save();
    }

    private void UpdateSettings()
    {
        // settingsData.resolution = Screen.currentResolution.ToString();
        settingsData.width = Screen.width;
        settingsData.height = Screen.height;
        settingsData.fullScreenMode = (int) Screen.fullScreenMode;
        settingsData.shadowQuality = (int) QualitySettings.shadows;
        settingsData.shadowDistance = QualitySettings.shadowDistance;
        settingsData.anisotropicFiltering = (int) QualitySettings.anisotropicFiltering;
        settingsData.antiAliasing = QualitySettings.antiAliasing;

        Debug.Log("JSON Settings: " + JsonUtility.ToJson(settingsData));
        
        // UpdateUI();
        EventManager.instance.UpdateVideoSettingsUI();
    }

    #region Save/Load

    [Serializable]
    public struct SettingsData
    {
        // public string resolution;
        public int width;
        public int height;
        public int fullScreenMode;
        public int shadowQuality;
        public float shadowDistance;
        public int anisotropicFiltering;
        public int antiAliasing;
    }
    
    public string Save()
    {
        // Apply();
        string fullPath = Path.Combine(Application.persistentDataPath, settingsFile);
        
        try
        {
            File.WriteAllText(fullPath, JsonUtility.ToJson(settingsData));

            // saveText.gameObject.SetActive(true);
            return "Saved";
        }
        catch (Exception e)
        {
            #if UNITY_EDITOR
                Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            #endif
            
            // saveText.gameObject.SetActive(true);
            return $"Failed to write to {fullPath} with exception {e}";
        }
        finally
        {
            Invoke("HideSaveText", 5);
        }
    }

    public void Load()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, settingsFile);
        
        try
        {
            string data = File.ReadAllText(fullPath);
            Debug.Log(data, this);
            
            // JsonUtility.FromJsonOverwrite(data, settingsData);
            settingsData = (SettingsData) JsonUtility.FromJson(data, typeof(SettingsData));
            
            // UpdateUI();
            EventManager.instance.UpdateVideoSettingsUI();
            // Apply();
            EventManager.instance.ApplyVideoSettings();
        }
        catch (Exception e)
        {
            #if UNITY_EDITOR
                Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            #endif
                
            SetDefaults();
        }
    }

    #endregion Save/Load
    
}
