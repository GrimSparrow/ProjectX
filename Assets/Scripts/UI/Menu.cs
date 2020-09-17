using ProjectX;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu : BaseWindow
{
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button toMainMeu;

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        Loader.Load(Loader.Scene.MainMenu);
    }
}
