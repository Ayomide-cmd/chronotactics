namespace ChronoTactics.Entities;

using System.Collections.Generic;
using ChronoTactics.Engine;

public class TimeClone
{
    public Vector2I Position { get; private set; }
    private List<PlayerAction> _recordedHistory;

    public TimeClone(List<PlayerAction> history)
    {
        _recordedHistory = new List<PlayerAction>(history);
        if (_recordedHistory.Count > 0)
        {
            Position = new Vector2I(_recordedHistory[0].Position.X, _recordedHistory[0].Position.Y);
        }
    }

    public void StepToTurn(int turn)
    {
        if (turn < 0 || _recordedHistory.Count == 0) return;

        if (turn < _recordedHistory.Count)
        {
            Position.X = _recordedHistory[turn].Position.X;
            Position.Y = _recordedHistory[turn].Position.Y;
        }
        else
        {
            Position.X = _recordedHistory[_recordedHistory.Count - 1].Position.X;
            Position.Y = _recordedHistory[_recordedHistory.Count - 1].Position.Y;
        }
    }
}