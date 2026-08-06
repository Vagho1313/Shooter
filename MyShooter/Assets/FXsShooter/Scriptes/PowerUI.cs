using System;
using UnityEngine;
using UnityEngine.UI;

namespace FXs.Shooter
{
    public class PowerUI : MonoBehaviour
    {
        [SerializeField] private Slider powerSlider;
        [SerializeField] private Text valueText;

        public event Action<int> OnSliderChanged;
        public int Value { get; private set; }

        public void Init()
        {
            powerSlider.onValueChanged.AddListener((float value) =>
            {
                Value = (int)value;
                OnSliderChanged?.Invoke(Value);
                valueText.text = Value.ToString("0");
            });
        }

        public void Setup(int value, int minValue, int maxValue)
        {
            Value = value;
            powerSlider.minValue = minValue;
            powerSlider.maxValue = maxValue;
            powerSlider.value = value;
            valueText.text = Value.ToString("0");
        }
    }
}
