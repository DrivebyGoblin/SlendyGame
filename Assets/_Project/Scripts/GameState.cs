public class GameState 
{
    private GameStates _currentState;
    public GameStates CurrentState { get => _currentState; }


    public void SetState(GameStates state)
    {
        if (state == _currentState) return;
        _currentState = state;
    }
}

public enum GameStates
{
    Gameplay,
    Cutscene,
    Paused
}
