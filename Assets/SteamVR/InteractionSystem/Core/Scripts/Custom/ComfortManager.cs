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
    public static ComfortManager instance;
    public static ComfortSettingsData settingsData;
    private string settingsFile = "ComfortSettings.dat";

    void Start()
    {
        // Debug.Log("ComfortManager Start");
        instance = this;
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
        settingsData.snapTurnAngle = 45;
        settingsData.pointerMode = 0;
        
        // UpdateUI();
        EventManager.instance.UpdateComfortSettingsUI();
        Save();
    }

    public PointerState GetPointerState()
    {
        switch (settingsData.pointerMode)
        {
            case 0:
                return PointerState.RATS;
            default:
                return PointerState.PhysicsPointer;
        }
    }
    
    #region Save/Load

    [Serializable]
    public struct ComfortSettingsData
    {
        public float speed;
        public int enableTeleportBlackout;
        public float tpBlackoutDuration;
        public int enableSnapTurnBlackout;
        public float stBlackoutDuration;
        public float snapTurnAngle;
        public int pointerMode;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
    
    public string Save()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, settingsFile);

        try
        {
            Debug.Log(settingsData);
            string json = JsonUtility.ToJson(settingsData);
            Debug.Log(json);

            File.WriteAllText(fullPath, json);

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
            Debug.Log("Comfort Data: " + data);
            
            // JsonUtility.FromJsonOverwrite(data, settingsData);
            settingsData = (ComfortSettingsData) JsonUtility.FromJson(data, typeof(ComfortSettingsData));
            Debug.Log("Comfort Load.settingsData: " + settingsData);
            
            // UpdateUI();
            EventManager.instance.UpdateComfortSettingsUI();
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
