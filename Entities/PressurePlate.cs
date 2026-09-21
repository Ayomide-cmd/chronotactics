namespace ChronoTactics.Entities;

using System.Collections.Generic;
using ChronoTactics.Engine;

public class PressurePlate
{
    public Vector2I Position { get; private set; }
    public bool IsPressed { get; private set; }

    public PressurePlate(int x, int y)
    {
        Position = new Vector2I(x, y);
        IsPressed = false;
    }

    public void UpdateState(Vector2I playerPos, List<Vector2I> clonePositions)
    {
        bool occupied = playerPos.Equals(Position);
        if (!occupied)
        {
            foreach (var pos in clonePositions)
            {
                if (pos.Equals(Position))
                {
                    occupied = true;
                    break;
                }
            }
        }
        IsPressed = occupied;
    }
}