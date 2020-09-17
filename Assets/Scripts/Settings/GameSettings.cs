using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Create GameSettings")]
public class GameSettings : ScriptableObject
{
    [Range(0f,200f)]
    [SerializeField]
    private float lookSensitivity = 100;

    private int qualitySettings;
    public float LookSensitivity
    {
        get => lookSensitivity;
        set => lookSensitivity = value;
    }

    public int QualitySettings
    {
        get => qualitySettings;
        set => qualitySettings = value;
    }
}
