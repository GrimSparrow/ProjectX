using ProjectX;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameBtn;
    [SerializeField] private Button HighQuality;
    [SerializeField] private Button LowQuality;
    private void Awake()
    {
        newGameBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        
        LowQuality.onClick.AddListener(() =>
        {
            QualitySettings.SetQualityLevel(0, true);
        });
        HighQuality.onClick.AddListener(() =>
        {
            QualitySettings.SetQualityLevel(5, true);
        });
    }

    public void GoToScene(Loader.Scene scene)
    {
        Loader.Load(Loader.Scene.GameScene);
    }
}

public static class SettingsManager
{
    public static void SetSettings (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }
}
