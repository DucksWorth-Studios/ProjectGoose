using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComfortManager : MonoBehaviour
{
    public TMP_Dropdown enableTPDropdown;
    public Slider tpDurationSlider;
    public TextMeshProUGUI tpDurationText;
    
    public TMP_Dropdown enableSTDropdown;
    public Slider stDurationSlider;
    public TextMeshProUGUI stDurationText;
    
    public TextMeshProUGUI saveText;
    
    private static ComfortSettingsData settingsData;
    private string settingsFile = "ComfortSettings.dat";

    #region Events

    public void OnChangeTPDuration(float tpDuration)
    {
        string strDuration = tpDuration.ToString("#.00");
        float duration = float.Parse(strDuration);
        
        tpDurationText.text = "" + duration;
        settingsData.tpBlackoutDuration = duration;

        // Debug.Log(duration);
        tpDurationSlider.value = duration;
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
        public bool enableTeleportBlackout;
        public float tpBlackoutDuration;
        public bool enableSnapTurnBlackout;
        public float stBlackoutDuration;
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
            Debug.Log(data);
            
            JsonUtility.FromJsonOverwrite(data, settingsData);
            // UpdateUI();
            // UpdateUI();
            // Apply();
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
        }
    }

    #endregion Save/Load
}
