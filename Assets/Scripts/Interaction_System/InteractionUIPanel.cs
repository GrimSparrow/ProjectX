using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ProjectX
{        
    public class InteractionUIPanel : MonoBehaviour
    {
        [SerializeField] private Image progressBar;
        [SerializeField] private GameObject Crosshair;
        private Sprite defaultCross;
        private Image cross;

        private void Start()
        {
            defaultCross = Crosshair.GetComponent<Image>().sprite;
            cross = Crosshair.GetComponent<Image>();
        }

        public void UpdateProgressBar(float fillAmount)
        {
            progressBar.fillAmount = fillAmount;
        }

        public void ResetUI()
        {
            progressBar.fillAmount = 0f;
            SetCrosshair(defaultCross);
        }

        public void SetVisibility(bool isActive)
        {
            //gameObject.SetActive(isActive);
            Crosshair.SetActive(isActive);
        }

        public void SetCrosshair(Sprite crossImg)
        {
            cross.sprite = crossImg;
        }
    }
}
