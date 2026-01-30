using UnityEngine;
using System;

public class PauseController : MonoBehaviour
{
    private PauseService _pauseService;
    private IInput _input;

    public event Action onButtonPressed;

    public void Initialize(PauseService pauseService, IInput input)
    {
        _input = input;
        _pauseService = pauseService;
        onButtonPressed += _pauseService.TogglePause;
    }

    

    public void SetPause()
    {
        onButtonPressed?.Invoke();
    }

    private void Update()
    {
        if (_input != null && _input.Pause())
        {
            SetPause();
        }
    }  
}



