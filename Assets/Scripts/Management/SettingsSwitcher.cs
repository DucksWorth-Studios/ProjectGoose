using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSwitcher : MonoBehaviour
{
    public GameObject videoSettings;
    public GameObject comfortSettings;

    public void ToggleVideoSettings()
    {
        videoSettings.SetActive(!videoSettings.activeSelf);
    }

    public void ToggleComfortSettings()
    {
        comfortSettings.SetActive(!comfortSettings.activeSelf);
    }
}
