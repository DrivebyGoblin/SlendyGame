using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private Action<float> _onValueChanged;

    public void Initialize(float minValue, float maxValue, float value, Action<float> onValueChanged)
    {
        _slider.minValue = minValue;
        _slider.maxValue = maxValue;

        if (_slider != null)
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        _onValueChanged = onValueChanged;
        _slider.value = value;
    }

    private void OnDisable()
    {
        if (_slider != null)
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        _onValueChanged?.Invoke(value);
    }




}
