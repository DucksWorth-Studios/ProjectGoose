using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Author: Cameron Scholes
/// Class for storing and using comfort settings
/// </summary>

public class ComfortManager : MonoBehaviour
{
    [Header("Movement Speed")]
    public Slider speedSlider;
    public TextMeshProUGUI speedValue;
    
    [Header("Teleport")]
    public TMP_Dropdown enableTPDropdown;
    public Slider tpDurationSlider;
    public TextMeshProUGUI tpDurationText;
    
    [Header("Snap Turn")]
    public TMP_Dropdown enableSTDropdown;
    public Slider stDurationSlider;
    public TextMeshProUGUI stDurationText;
    
    [Header("Other")]
    public TextMeshProUGUI saveText;
    public GameObject startMenu;
    public GameObject optionsMenu;
    
    public static ComfortSettingsData settingsData;
    private string settingsFile = "ComfortSettings.dat";

    void Start()
    {
        Debug.Log("ComfortManager Start");
        settingsData = new ComfortSettingsData();
        Load();
        // SetDefaults();
    }

    private void SetDefaults()
    {
        settingsData.speed = 3;
        settingsData.enableTeleportBlackout = 0;
        settingsData.tpBlackoutDuration = 0;
        settingsData.enableSnapTurnBlackout = 0;
        settingsData.stBlackoutDuration = 0;
        
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        Debug.Log("UpdateUI.settingsData: " + settingsData);
        Debug.Log("ComfortManager.settingsData: " + ComfortManager.settingsData);
        OnChangeSpeed(settingsData.speed);
        
        enableTPDropdown.value = settingsData.enableTeleportBlackout;
        enableTPDropdown.RefreshShownValue();
        OnChangeTPDuration(settingsData.tpBlackoutDuration);
        
        enableSTDropdown.value = settingsData.enableSnapTurnBlackout;
        enableSTDropdown.RefreshShownValue();
        OnChangeSTDuration(settingsData.stBlackoutDuration);
        
        gameObject.SetActive(false);
    }
    
    public void Close()
    {
        optionsMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);
    }

    #region Events

    public void OnChangeSpeed(float newSpeed)
    {
        string strSpeed = newSpeed.ToString("#.00");
        float speed = float.Parse(strSpeed);
        
        speedValue.text = "" + speed;
        settingsData.speed = speed;

        // Debug.Log(speed);
        speedSlider.value = speed;
    }
    
    public void OnChangeTPBlackout(int blackout)
    {
        Debug.Log(blackout);
        settingsData.enableTeleportBlackout = blackout;
    }
    
    public void OnChangeTPDuration(float tpDuration)
    {
        string strDuration = tpDuration.ToString("#.00");
        float duration = float.Parse(strDuration);
        
        tpDurationText.text = "" + duration;
        settingsData.tpBlackoutDuration = duration;

        // Debug.Log(duration);
        tpDurationSlider.value = duration;
    }
    
    public void OnChangeSTBlackout(int blackout)
    {
        Debug.Log(blackout);
        settingsData.enableSnapTurnBlackout = blackout;
    }
    
    public void OnChangeSTDuration(float stDuration)
    {
        string strDuration = stDuration.ToString("#.00");
        float duration = float.Parse(strDuration);
        
        stDurationText.text = "" + duration;
        settingsData.stBlackoutDuration = duration;
        
        // Debug.Log(duration);
        stDurationSlider.value = duration;
    }

    #endregion
    
    #region Save/Load

    [Serializable]
    public struct ComfortSettingsData
    {
        public float speed;
        public int enableTeleportBlackout;
        public float tpBlackoutDuration;
        public int enableSnapTurnBlackout;
        public float stBlackoutDuration;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
    
    public void Save()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, settingsFile);
        
        try
        {
            Debug.Log(settingsData);
            string json = JsonUtility.ToJson(settingsData);
            Debug.Log(json);
            
            File.WriteAllText(fullPath, json);

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
    
    private void HideSaveText()
    {
        saveText.gameObject.SetActive(false);
    }

    private void Load()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, settingsFile);
        
        try
        {
            string data = File.ReadAllText(fullPath);
            Debug.Log("Data: " + data);
            
            // JsonUtility.FromJsonOverwrite(data, settingsData);
            settingsData = (ComfortSettingsData) JsonUtility.FromJson(data, typeof(ComfortSettingsData));
            Debug.Log("Load.settingsData: " + settingsData);
            UpdateUI();
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            SetDefaults();
        }
    }

    #endregion Save/Load
}
