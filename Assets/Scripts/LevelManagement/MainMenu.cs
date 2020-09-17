using ProjectX;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameBtn;
    private void Awake()
    {
        newGameBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
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
