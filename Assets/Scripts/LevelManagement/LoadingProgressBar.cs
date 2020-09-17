
using System;
using ProjectX;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class LoadingProgressBar : MonoBehaviour
{
    [SerializeField]private Image image;
    [SerializeField] [TextArea] private string[] Tips;
    [SerializeField] private TextMeshProUGUI tip;
    private void Start()
    {
        Random random = new Random();
        
        tip.text = Tips[random.Next(0, Tips.Length)];
    }

    private void Update()
    {
        image.fillAmount = Loader.GetLoadingProgress();
    }
}
