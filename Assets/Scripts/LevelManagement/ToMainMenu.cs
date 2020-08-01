using ProjectX;
using UnityEngine;
using UnityEngine.UI;

public class ToMainMenu : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenu);
        });
    }
}
