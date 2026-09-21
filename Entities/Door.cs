namespace ChronoTactics.Entities;

using ChronoTactics.Engine;

public class Door
{
    public Vector2I Position { get; private set; }
    public bool IsOpen { get; private set; }

    public Door(int x, int y)
    {
        Position = new Vector2I(x, y);
        IsOpen = false;
    }

    public void SetOpenState(bool open)
    {
        IsOpen = open;
    }
}