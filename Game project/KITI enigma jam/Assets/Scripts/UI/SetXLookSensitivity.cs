using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SetXLookSensitivity : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private TextMeshProUGUI text;

        private void Start()
        {
            text.text = "Sensitivity: 100%";
            slider.onValueChanged.AddListener(SetXLookSpeedMultiplier);
            slider.onValueChanged.AddListener(SetYLookSpeedMultiplier);
        }

        private void SetXLookSpeedMultiplier(float value)
        {
            text.text = $"Sensitivity: {value:F0}%";
            firstPersonController.lookSpeedXMultiplier = value * 0.01f;
        }
        
        private void SetYLookSpeedMultiplier(float value)
        {
            firstPersonController.lookSpeedYMultiplier = value * 0.01f;
        }
    }
}