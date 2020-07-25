using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ProjectX
{        
    public class InteractionUIPanel : MonoBehaviour
    {
        [SerializeField] private Image progressBar;
        [SerializeField] private TextMeshProUGUI tooltipText;
        [SerializeField] private GameObject Crosshair;

        public void SetTooltip(string tooltip)
        {
            tooltipText.SetText(tooltip);
        }

        public void UpdateProgressBar(float fillAmount)
        {
            progressBar.fillAmount = fillAmount;
        }

        public void ResetUI()
        {
            progressBar.fillAmount = 0f;
            tooltipText.SetText("");
        }

        public void SetVisibility(bool isActive)
        {
            gameObject.SetActive(isActive);
            Crosshair.SetActive(isActive);
        }
    }
}
