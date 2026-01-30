using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/CameraControllerSettings")]
public class PlayerCameraSettings : ScriptableObject
{
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;

    public float MinValue { get => _minValue; }
    public float MaxValue { get => _maxValue; }
}
