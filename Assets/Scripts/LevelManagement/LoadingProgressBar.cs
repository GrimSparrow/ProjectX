
using System;
using ProjectX;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBar : MonoBehaviour
{
    [SerializeField]private Image image;


    private void Update()
    {
        image.fillAmount = Loader.GetLoadingProgress();
    }
}
