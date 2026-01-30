using System;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class PlayerFearMeter : MonoBehaviour, IDisable, IPausable
{
    private const float MinValue = 0;
    private const float MaxValue = 100;
    private FearMeterSpeedConfig _speedConfig;
    private EnemyFollowAgent _target;
    private LoseGame _loseGame;
    private Camera _camera;
    private float _upSpeed;
    private float _downSpeed;
    private float _tempValue;
    private float _currentValue = 0;
    private bool _targetState;

    public float Value { get => _currentValue; }
    public bool IsObserved { get; private set; }
    public bool IsPaused { get; private set; }

    public event Action onValueReached;

    public void Initialize(EnemyFollowAgent target, FearMeterSpeedConfig config, LoseGame lose)
    {
        _camera = Camera.main;
        _target = target;
        _speedConfig = config;
        _downSpeed = config.DownSpeed;
        _loseGame = lose;
        onValueReached += _loseGame.Lose;
    }


    private void OnDisable()
    {
        onValueReached -= _loseGame.Lose;
    }


    public void ChangeSpeed()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);

        foreach (var item in _speedConfig.Config)
        {
            if (distance >= item.MinDistance && distance <= item.MaxDistance)
            {
                _upSpeed = item.Speed;
                break;
            }
        }
    }

    private void UpdateValueTowardsTarget()
    {
        _targetState = IsObjectVisible(_target);
        float targetValue = _targetState ? MaxValue : MinValue;
        float currentSpeed = (targetValue > _currentValue) ? _upSpeed : _downSpeed;
        _currentValue = Mathf.MoveTowards(_currentValue, targetValue, currentSpeed * Time.deltaTime);
        ChekValue();
    }


    private bool IsObjectVisible(EnemyFollowAgent target)
    {

        Vector3 targetPos = target.transform.position;

        Vector3 viewportPoint = _camera.WorldToViewportPoint(targetPos);
        bool isWithinSight = viewportPoint.z > 0 &&
                        viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                        viewportPoint.y >= 0 && viewportPoint.y <= 1;

        if (!isWithinSight)
        {
            IsObserved = false;
            return false;
        }

        else
        {
            if (IsObstructed(target.transform, targetPos))
            {
                IsObserved = false;
                return false;
            }
            else
            {
                IsObserved = true;
                return true;
            }
        }
    }

    private bool IsObstructed(Transform target, Vector3 targetPos)
    {
        Vector3 screenPoint = _camera.WorldToScreenPoint(targetPos);
        Ray ray = _camera.ScreenPointToRay(screenPoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == target)
                return false;
            else
                return true;
        }
        return false;
    }


    private void Update()
    {

        if (!IsPaused)
        {
            ChangeSpeed();
            UpdateValueTowardsTarget();
        }
        else
        {
            return;
        }
    }

    private void ChekValue()
    {
        if (_currentValue >= MaxValue) 
        {
            onValueReached.Invoke();
        }
    }

    public void Disable()
    {
        _currentValue = MinValue;
        _target.gameObject.SetActive(false);
    }

    public void Resume()
    {
        IsPaused = false;
        _currentValue = _tempValue;
        _tempValue = 0;
    }

    public void Pause()
    {
        IsPaused = true;
        _tempValue = _currentValue;
        _currentValue = 0;
    }
}
