namespace ChronoTactics.Systems;

using System.Collections.Generic;
using ChronoTactics.Entities;
using ChronoTactics.Engine;

public class TimeRewindSystem
{
    public int CurrentTurn { get; private set; }
    public List<PlayerAction> CurrentTimeline { get; private set; }
    public List<TimeClone> ActiveClones { get; private set; }

    public TimeRewindSystem()
    {
        CurrentTurn = 0;
        CurrentTimeline = new List<PlayerAction>();
        ActiveClones = new List<TimeClone>();
    }

    public void RecordStep(Vector2I playerPos)
    {
        CurrentTimeline.Add(new PlayerAction(CurrentTurn, new Vector2I(playerPos.X, playerPos.Y)));
        CurrentTurn++;
    }

    public Vector2I ExecuteRewind(Vector2I spawnPos)
    {
        if (CurrentTimeline.Count > 0)
        {
            ActiveClones.Add(new TimeClone(CurrentTimeline));
        }

        CurrentTimeline.Clear();
        CurrentTurn = 0;
        
        return new Vector2I(spawnPos.X, spawnPos.Y);
    }

    public void UpdateClones()
    {
        foreach (var clone in ActiveClones)
        {
            clone.StepToTurn(CurrentTurn);
        }
    }

    public List<Vector2I> GetClonePositions()
    {
        List<Vector2I> positions = new List<Vector2I>();
        foreach (var clone in ActiveClones)
        {
            positions.Add(clone.Position);
        }
        return positions;
    }
}