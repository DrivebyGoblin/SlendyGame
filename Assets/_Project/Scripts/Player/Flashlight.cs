using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float _minSpotAngle = 10f;
    [SerializeField] private float _maxSpotAngle = 60f;
    [SerializeField] private float _minRange = 10f;
    [SerializeField] private float _maxRange = 50f;
    [SerializeField] private float _minIntensity = 0.5f;
    [SerializeField] private float _maxIntensity = 2f;
    [SerializeField] private float _speed = 20f;
    private Light _light;
    private IInput _input;


    

    public void Initialize(IInput input)
    {
        _light = GetComponent<Light>();
        _input = input;

    }

    private void Update()
    {
        UpdateFlashlightProperties();
    }

    private void UpdateFlashlightProperties()
    {
        if (_light == null) return;

        if (_input.RightClick())
        {
            SetFlashlightProperties(
                _minSpotAngle, _maxRange, _maxIntensity);
        }
        else
        {
            SetFlashlightProperties(
                _maxSpotAngle, _minRange, _minIntensity);
        }
    }

    private void SetFlashlightProperties(float targetSpotAngle, float targetRange, float targetIntensity)
    {
        _light.spotAngle = Mathf.Lerp(_light.spotAngle, targetSpotAngle, _speed * Time.deltaTime);
        _light.range = Mathf.Lerp(_light.range, targetRange, _speed * Time.deltaTime);
        _light.intensity = Mathf.Lerp(_light.intensity, targetIntensity, _speed * Time.deltaTime);
    }
}
