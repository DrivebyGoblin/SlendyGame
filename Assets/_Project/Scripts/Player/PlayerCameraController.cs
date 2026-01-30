using UnityEngine;

public class PlayerCameraController : MonoBehaviour, IPausable
{
    private CharacterController _player;
    private IInput _input;
    private float _sensetivity;
    private float _xRotation;
    public bool IsPaused { get; private set; }

    public void Initialize(IInput input, CharacterController player)
    {
        _player = player;
        _input = input;
        _sensetivity = SensitivityData.Sensetivity;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Look()
    {
        float deltaX = _input.GetPointerDeltaX() * _sensetivity * Time.deltaTime;
        float deltaY = _input.GetPointerDeltaY() * _sensetivity * Time.deltaTime;

        _xRotation -= deltaY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _player.transform.Rotate(Vector3.up * deltaX);
    }

    

    private void Update()
    {
        if (!IsPaused)
            Look();
        else
            return;
    }

    public void Pause()
    {
        IsPaused = true;
    }

    public void Resume()
    {
        IsPaused = false;
    }
}
