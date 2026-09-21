namespace ChronoTactics.Engine;

public enum GameCommand
{
    None,
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    RewindTime,
    Reset
}

public class InputManager
{
    public GameCommand ParseInputKey(string keyKey)
    {
        return keyKey switch
        {
            "W" or "Up" => GameCommand.MoveUp,
            "S" or "Down" => GameCommand.MoveDown,
            "A" or "Left" => GameCommand.MoveLeft,
            "D" or "Right" => GameCommand.MoveRight,
            "R" => GameCommand.RewindTime,
            "Space" => GameCommand.Reset,
            _ => GameCommand.None
        };
    }
}