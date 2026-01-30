using System;
using System.Collections.Generic;


public class PauseService
{
    private readonly List<IPausable> _handlers = new List<IPausable>();
    private GameState _gameState;
    private bool _isPaused;
    public bool IsPaused { get => _isPaused; }

    public event Action OnPause;
    public event Action OnResume;


    public PauseService(GameState state)
    {
        _gameState = state;
    }


    public void TogglePause()
    {
        if (IsPaused)
            Resume();
        else
            Pause();
    }


    public void Pause()
    {
        if (_gameState.CurrentState == GameStates.Gameplay)
        {
            _isPaused = true;
            OnPause?.Invoke();

            foreach (var item in _handlers)
            {
                item.Pause();
            }
        }
    }



    public void Resume()
    {
        _isPaused = false;
        OnResume?.Invoke();

        foreach (var item in _handlers)
        {
            item.Resume();
        }
    }


    public void Register(IPausable handler) => _handlers.Add(handler);

    public void UnRegister(IPausable handler) => _handlers.Remove(handler);

}


