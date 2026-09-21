namespace ChronoTactics.Entities;

using ChronoTactics.Engine;

public class PlayerAction
{
    public int Turn { get; set; }
    public Vector2I Position { get; set; }

    public PlayerAction(int turn, Vector2I position)
    {
        Turn = turn;
        Position = position;
    }
}

public class Player
{
    public Vector2I Position { get; set; }

    public Player(int startX, int startY)
    {
        Position = new Vector2I(startX, startY);
    }

    public bool TryMove(int dx, int dy, GridMap grid, bool isDoorOpen)
    {
        int targetX = Position.X + dx;
        int targetY = Position.Y + dy;

        if (grid.IsWalkable(targetX, targetY, isDoorOpen))
        {
            Position.X = targetX;
            Position.Y = targetY;
            return true;
        }
        return false;
    }
}