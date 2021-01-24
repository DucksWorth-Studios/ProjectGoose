using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSwitcher : MonoBehaviour
{
    public GameObject videoSettings;
    public GameObject comfortSettings;

    void Start()
    {
        videoSettings.SetActive(false);
        comfortSettings.SetActive(false);
    }

    public void ToggleVideoSettings()
    {
        videoSettings.SetActive(!videoSettings.activeSelf);
    }

    public void ToggleComfortSettings()
    {
        comfortSettings.SetActive(!comfortSettings.activeSelf);
    }
}
