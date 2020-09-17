using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private TMP_Dropdown qualitySettings;
    [SerializeField] private Slider sensitivitySettings;
    private void Awake()
    {
        
        ResumeGameSettings();
    }

    private void ResumeGameSettings()
    {
        qualitySettings.value = gameSettings.QualitySettings;
        sensitivitySettings.value = gameSettings.LookSensitivity;
    }

    public void SetQualitySettings(TMP_Dropdown dropdown)
    {
        SettingsManager.SetSettings(dropdown.value);
        gameSettings.QualitySettings = dropdown.value;
    }

    public void SetLookSensitivity(Slider slider)
    {
        gameSettings.LookSensitivity = slider.value;
    }
}
