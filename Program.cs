namespace ChronoTactics;

using System;
using ChronoTactics.Engine;
using ChronoTactics.Scenes;
using ChronoTactics.Systems;

public class Program
{
    public static void Main(string[] args)
    {
        GameScene scene = new GameScene();
        InputManager input = new InputManager();

        GameLoop loop = new GameLoop(
            onUpdate: () =>
            {
                if (scene.State.CurrentState == GameState.Victory)
                {
                    Console.WriteLine("LEVEL COMPLETED.");
                }
            },
            onRender: () =>
            {
                Console.Clear();
                Console.WriteLine($"Turn: {scene.RewindSystem.CurrentTurn} | Clones: {scene.RewindSystem.ActiveClones.Count}");
                Console.WriteLine($"Plate Pressed: {scene.Plate.IsPressed} | Door Open: {scene.ExitDoor.IsOpen}");
                Console.WriteLine("Controls: WASD (Move), R (Rewind Timeline)");
                
                RenderAsciiGrid(scene);
            }
        );

        loop.Start();
        loop.Tick();

        while (loop.IsRunning)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            GameCommand cmd = input.ParseInputKey(keyInfo.Key.ToString());
            
            scene.HandleCommand(cmd);
            loop.Tick();
        }
    }

    private static void RenderAsciiGrid(GameScene scene)
    {
        for (int y = 0; y < scene.Grid.Height; y++)
        {
            for (int x = 0; x < scene.Grid.Width; x++)
            {
                Vector2I currentPos = new Vector2I(x, y);

                if (scene.MainPlayer.Position.Equals(currentPos))
                {
                    Console.Write(" P ");
                }
                else if (scene.RewindSystem.GetClonePositions().Contains(currentPos))
                {
                    Console.Write(" C ");
                }
                else if (scene.TargetGoal.Equals(currentPos))
                {
                    Console.Write(" G ");
                }
                else if (scene.ExitDoor.Position.Equals(currentPos))
                {
                    Console.Write(scene.ExitDoor.IsOpen ? " / " : " D ");
                }
                else if (scene.Plate.Position.Equals(currentPos))
                {
                    Console.Write(scene.Plate.IsPressed ? " o " : " O ");
                }
                else if (scene.Grid.GetTile(x, y) == TileType.Wall)
                {
                    Console.Write("###");
                }
                else
                {
                    Console.Write(" . ");
                }
            }
            Console.WriteLine();
        }
    }
}