using UnityEngine;

public class PlayerFootsteps : MonoBehaviour, IDisable
{
    [SerializeField] private LayerMask _groundLayerMask;
    private MovementHandler _movementHandler;
    private Ray _ray;
    private float _timeBetweenSteps;
    private float _nextStepTime;
    private float _currentSpeed;
    private float _rayLength = 25f;
    private float _normalizedSpeedDivider = 10f;
    private float _minSpeedFactor = 0.2f;
    private float _maxSpeedFactor = 1f;


    public void Initialize()
    {
        _movementHandler = GetComponent<MovementHandler>();
    }

    private void Update()
    {
        HandleFootsteps();
    }


    private void HandleFootsteps()
    {
        _ray = new Ray(transform.position, Vector3.down * _rayLength);
        _currentSpeed = _movementHandler.CurrentSpeed;
        if (_currentSpeed > 0f && _movementHandler.IsMoving)
        {
            _timeBetweenSteps = CalculateStepInterval(_currentSpeed);

            if (Time.time >= _nextStepTime)
            {
                PlayFootstepSound();
                _nextStepTime = Time.time + _timeBetweenSteps;
            }
        }
    }

    private float CalculateStepInterval(float speed)
    {
        float baseStepTime = 0.5f;
        float speedFactor = Mathf.Clamp(speed / _normalizedSpeedDivider, _minSpeedFactor, _maxSpeedFactor);
        return baseStepTime / speedFactor;
    }

    private void PlayFootstepSound()
    {
        int surfaceLayer = GetSurfaceLayer();
        GroupSFX groupToPlay = GetSFXGroupByLayer(surfaceLayer);
        SFXService.Instance.PlayRandomClipFromGroup(groupToPlay);
    }


    private int GetSurfaceLayer()
    {
        if (Physics.Raycast(_ray.origin, _ray.direction, out RaycastHit hit, _rayLength, _groundLayerMask))
            return hit.collider.gameObject.layer;
            
        return 0;
    }


    private GroupSFX GetSFXGroupByLayer(int layer)
    {
        switch (layer)
        {
            case var l when l == SurfaceLayers.GetLayerValue(LayerType.Grass):
                return GroupSFX.GrassFootsteps;
            case var l when l == SurfaceLayers.GetLayerValue(LayerType.Wood):
                return GroupSFX.WoodFootsteps;
            default:
                return GroupSFX.GrassFootsteps;
        }
    }


    public void Disable()
    {
        this.enabled = false;
    }    

}
