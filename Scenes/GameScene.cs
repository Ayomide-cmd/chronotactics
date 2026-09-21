namespace ChronoTactics.Scenes;

using System.Collections.Generic;
using ChronoTactics.Engine;
using ChronoTactics.Entities;
using ChronoTactics.Systems;

public class GameScene
{
    public GridMap Grid { get; private set; }
    public Player MainPlayer { get; private set; }
    public PressurePlate Plate { get; private set; }
    public Door ExitDoor { get; private set; }
    public Vector2I TargetGoal { get; private set; }
    
    public TimeRewindSystem RewindSystem { get; private set; }
    public StateMachine State { get; private set; }

    private Vector2I _startPos;

    public GameScene()
    {
        Grid = new GridMap(8, 8);
        _startPos = new Vector2I(1, 1);
        MainPlayer = new Player(_startPos.X, _startPos.Y);
        
        Plate = new PressurePlate(2, 5);
        ExitDoor = new Door(5, 3);
        TargetGoal = new Vector2I(6, 3);

        Grid.SetTile(Plate.Position.X, Plate.Position.Y, TileType.Plate);
        Grid.SetTile(ExitDoor.Position.X, ExitDoor.Position.Y, TileType.Door);

        RewindSystem = new TimeRewindSystem();
        State = new StateMachine();

        RewindSystem.RecordStep(MainPlayer.Position);
    }

    public void HandleCommand(GameCommand cmd)
    {
        if (State.CurrentState == GameState.Victory) return;

        int dx = 0;
        int dy = 0;

        switch (cmd)
        {
            case GameCommand.MoveUp: dy = -1; break;
            case GameCommand.MoveDown: dy = 1; break;
            case GameCommand.MoveLeft: dx = -1; break;
            case GameCommand.MoveRight: dx = 1; break;
            case GameCommand.RewindTime:
                TriggerRewind();
                return;
        }

        if (dx != 0 || dy != 0)
        {
            bool moved = MainPlayer.TryMove(dx, dy, Grid, ExitDoor.IsOpen);
            if (moved)
            {
                RewindSystem.RecordStep(MainPlayer.Position);
                RewindSystem.UpdateClones();
                
                Plate.UpdateState(MainPlayer.Position, RewindSystem.GetClonePositions());
                ExitDoor.SetOpenState(Plate.IsPressed);

                CheckWinCondition();
            }
        }
    }

    private void TriggerRewind()
    {
        State.ChangeState(GameState.Rewinding);
        Vector2I resetPos = RewindSystem.ExecuteRewind(_startPos);
        MainPlayer.Position = new Vector2I(resetPos.X, resetPos.Y);
        
        RewindSystem.RecordStep(MainPlayer.Position);
        RewindSystem.UpdateClones();
        
        Plate.UpdateState(MainPlayer.Position, RewindSystem.GetClonePositions());
        ExitDoor.SetOpenState(Plate.IsPressed);
        
        State.ChangeState(GameState.Planning);
    }

    private void CheckWinCondition()
    {
        if (MainPlayer.Position.Equals(TargetGoal))
        {
            State.ChangeState(GameState.Victory);
        }
    }
}