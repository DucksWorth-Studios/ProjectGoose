using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComfortSettingsMenu : MonoBehaviour
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
    public Slider stAngleSlider;
    public TextMeshProUGUI stAngleText;
    
    [Header("Pointer")]
    public TMP_Dropdown pointerDropdown;
    
    [Header("Other")]
    public TextMeshProUGUI saveText;
    public GameObject startMenu;
    public GameObject optionsMenu;

    public void Awake()
    {
        EventManager.instance.OnUpdateComfortSettingsUI += UpdateUI;
        Debug.LogWarning("Comfort event registered");
    }

    // private void OnEnable()
    // {
    //     ComfortManager.instance.Load();
    // }

    private void UpdateUI()
    {
        // Debug.Log("UpdateUI.settingsData: " + ComfortManager.settingsData);
        Debug.Log("ComfortManager.settingsData: " + ComfortManager.settingsData);
        OnChangeSpeed(ComfortManager.settingsData.speed);
        
        enableTPDropdown.value = ComfortManager.settingsData.enableTeleportBlackout;
        enableTPDropdown.RefreshShownValue();
        OnChangeTPDuration(ComfortManager.settingsData.tpBlackoutDuration);
        
        enableSTDropdown.value = ComfortManager.settingsData.enableSnapTurnBlackout;
        enableSTDropdown.RefreshShownValue();

        pointerDropdown.value = ComfortManager.settingsData.pointerMode;
        pointerDropdown.RefreshShownValue();
        
        OnChangeSTDuration(ComfortManager.settingsData.stBlackoutDuration);
        OnChangeSTAngle(ComfortManager.settingsData.snapTurnAngle);
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
        ComfortManager.settingsData.speed = speed;

        // Debug.Log(speed);
        speedSlider.value = speed;
    }
    
    public void OnChangeTPBlackout(int blackout)
    {
        // Debug.Log(blackout);
        ComfortManager.settingsData.enableTeleportBlackout = blackout;
    }
    
    public void OnChangeTPDuration(float tpDuration)
    {
        string strDuration = tpDuration.ToString("#.00");
        float duration = float.Parse(strDuration);
        
        tpDurationText.text = "" + duration;
        ComfortManager.settingsData.tpBlackoutDuration = duration;

        // Debug.Log(duration);
        tpDurationSlider.value = duration;
    }
    
    public void OnChangeSTBlackout(int blackout)
    {
        // Debug.Log(blackout);
        ComfortManager.settingsData.enableSnapTurnBlackout = blackout;
    }
    
    public void OnChangeSTDuration(float stDuration)
    {
        string strDuration = stDuration.ToString("#.00");
        float duration = float.Parse(strDuration);
        
        stDurationText.text = "" + duration;
        ComfortManager.settingsData.stBlackoutDuration = duration;
        
        // Debug.Log(duration);
        stDurationSlider.value = duration;
    }
    
    public void OnChangeSTAngle(float stAngle)
    {
        stAngleText.text = "" + stAngle;
        ComfortManager.settingsData.snapTurnAngle = stAngle;
        
        // Debug.Log(stAngle);
        stAngleSlider.value = stAngle;
    }
    
    public void OnChangePointerMode(int pointer)
    {
        Debug.Log(pointer);
        ComfortManager.settingsData.pointerMode = pointer;
    }
    
    private void HideSaveText()
    {
        saveText.gameObject.SetActive(false);
    }

    #endregion
    
    public void Save()
    {
        saveText.gameObject.SetActive(true);
        saveText.text = ComfortManager.instance.Save();
        
        Invoke("HideSaveText", 5);
    }
}
