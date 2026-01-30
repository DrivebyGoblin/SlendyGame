using UnityEngine;

public class PlayerSpeedController
{
    private float _accelerationTime = 0.5f;
    private float _maxSpeed = 13f;
    private float _defaultSpeed;

    public void Initialize(float defaultSpeed)
    {
        _defaultSpeed = defaultSpeed;
    }

    public void Acceleration(bool isSprint, ref float CurrentPlayerSpeed)
    {
        float targetSpeed = isSprint ? _maxSpeed : _defaultSpeed;
        CurrentPlayerSpeed = Mathf.Lerp(CurrentPlayerSpeed, targetSpeed, Time.deltaTime / _accelerationTime);
    }
}
