using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComfortManager : MonoBehaviour
{
    public Slider speedSlider;
    public TextMeshProUGUI speedValue;
    
    public TMP_Dropdown enableTPDropdown;
    public Slider tpDurationSlider;
    public TextMeshProUGUI tpDurationText;
    
    public TMP_Dropdown enableSTDropdown;
    public Slider stDurationSlider;
    public TextMeshProUGUI stDurationText;
    
    public TextMeshProUGUI saveText;

    public static ComfortManager instance;
    private static ComfortSettingsData settingsData;
    private string settingsFile = "ComfortSettings.dat";

    void Start()
    {
        instance = this;
        settingsData = new ComfortSettingsData();

        Load();
    }

    private void UpdateUI()
    {
        Debug.Log("UpdateUI.settingsData: " + settingsData);
        OnChangeSpeed(settingsData.speed);
        
        enableTPDropdown.value = settingsData.enableTeleportBlackout;
        enableTPDropdown.RefreshShownValue();
        OnChangeTPDuration(settingsData.tpBlackoutDuration);
        
        enableSTDropdown.value = settingsData.enableSnapTurnBlackout;
        enableSTDropdown.RefreshShownValue();
        OnChangeSTDuration(settingsData.stBlackoutDuration);
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
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
            
            JsonUtility.FromJsonOverwrite(data, settingsData);
            settingsData = (ComfortSettingsData) JsonUtility.FromJson(data, typeof(ComfortSettingsData));
            Debug.Log("Load.settingsData: " + settingsData);
            UpdateUI();
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
        }
    }

    #endregion Save/Load
}
