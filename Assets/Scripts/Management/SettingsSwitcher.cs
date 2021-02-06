using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSwitcher : MonoBehaviour
{
    public GameObject videoSettings;
    public GameObject comfortSettings;

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
