using UnityEngine;

public class SettingsSwitcher : MonoBehaviour
{
    public GameObject videoSettings;
    public GameObject comfortSettings;
    public GameObject menu;
    public GameObject additionalMenu;

    private int count = 0;
    
    public void Awake()
    {
        EventManager.instance.OnUpdateVideoSettingsUI += UpdateCount;
        EventManager.instance.OnUpdateComfortSettingsUI += UpdateCount;
    }

    private void UpdateCount()
    {
        count++;
        // Debug.Log("Count: " + count, this);

        if (count == 2)
        {
            // Debug.Log("Count Max: " + count, this);
            EventManager.instance.OnUpdateVideoSettingsUI -= UpdateCount;
            EventManager.instance.OnUpdateComfortSettingsUI -= UpdateCount;
            
            ToggleComfortSettings();
            menu.SetActive(false);

            if (additionalMenu)
                additionalMenu.SetActive(false);
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
