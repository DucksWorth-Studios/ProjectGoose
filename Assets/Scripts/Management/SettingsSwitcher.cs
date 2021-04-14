using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSwitcher : MonoBehaviour
{
    public GameObject videoSettings;
    public GameObject comfortSettings;
    public GameObject menu;

    private int count = 0;
    
    public void Awake()
    {
        EventManager.instance.OnUpdateVideoSettingsUI += UpdateCount;
        EventManager.instance.OnUpdateComfortSettingsUI += UpdateCount;
    }

    private void UpdateCount()
    {
        count++;
        Debug.Log("Count: " + count, this);

        if (count == 2)
        {
            EventManager.instance.OnUpdateVideoSettingsUI -= UpdateCount;
            EventManager.instance.OnUpdateComfortSettingsUI -= UpdateCount;
            
            ToggleComfortSettings();
            menu.SetActive(false);
        }
    }

    public void ToggleVideoSettings()
    {
        videoSettings.SetActive(true);
        comfortSettings.SetActive(false);
    }

    public void ToggleComfortSettings()
    {
        videoSettings.SetActive(false);
        comfortSettings.SetActive(true);
    }
}
