
public static class SensitivityData
{
    private static float _sensetivity;
    private static float _minValue;
    private static float _maxValue;

    public static float MinValue { get => _minValue; }
    public static float MaxValue { get => _maxValue; }

    public static void Initialize(PlayerCameraSettings settings)
    {
        _minValue = settings.MinValue;
        _maxValue = settings.MaxValue;
    }

    public static float Sensetivity
    {
        get => _sensetivity;
        set => _sensetivity = value;
    }
}