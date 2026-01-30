using UnityEngine;

public class MovementHandler : MonoBehaviour, IDisable, IPausable
{
    private IInput _input;
    private CharacterController _controller;
    private PlayerSpeedController _playerSpeedController;
    private GameState _gameState;

    private float _currentSpeed;
    private float _walkSpeed = 8f;
    public float CurrentSpeed { get => _currentSpeed; }
    public bool IsMoving { get; set; }

    public bool IsPaused { get; private set; }

    public void Initialize(IInput input, GameState state)
    {
        _controller = GetComponent<CharacterController>();
        _playerSpeedController = new PlayerSpeedController();
        _playerSpeedController.Initialize(_walkSpeed);
        _currentSpeed = _walkSpeed;
        _input = input;
        _gameState = state;
    }



    public void Move()
    {
        if (_gameState.CurrentState == GameStates.Gameplay)
        {
            float horizontal = _input.GetHorizontalInput();
            float vertical = _input.GetVerticalInput();

            Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
            if (moveDirection.magnitude > 0f)
            {
                IsMoving = true;
            }
            else
            {
                IsMoving = false;
            }

            _controller.Move(moveDirection.normalized * CurrentSpeed * Time.deltaTime);
        }
        
    }

   

    private void Update()
    {
        if (!IsPaused)
        {
            _playerSpeedController.Acceleration(_input.IsSprinting(), ref _currentSpeed);
            Move();
        }
             
    }

    public void Disable()
    {
        this.enabled = false;
    }

    public void Pause()
    {
        IsMoving = false;
        IsPaused = true;
    }

    public void Resume()
    {
        IsPaused = false;
    }


}










