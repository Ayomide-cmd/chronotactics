namespace ChronoTactics.Systems;

public enum GameState
{
    Planning,
    Executing,
    Rewinding,
    Victory
}

public class StateMachine
{
    public GameState CurrentState { get; private set; }

    public StateMachine()
    {
        CurrentState = GameState.Planning;
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
    }
}