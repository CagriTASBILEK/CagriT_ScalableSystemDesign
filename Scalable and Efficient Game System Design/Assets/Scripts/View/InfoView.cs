using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace View
{
    public class InfoView : MonoBehaviour
    {
       // [SerializeField] private Slider infoSlider;
        [SerializeField] private TextMeshProUGUI playerHealthText;

        public void UpdateUI(int currentValue, int maxValue)
        {
           // infoSlider.value = (float)currentValue / maxValue;
           playerHealthText.text = $"{currentValue}/{maxValue}";
        }
    }
}