namespace ChronoTactics.Engine;

using System;
using System.Collections.Generic;

public enum TileType
{
    Empty,
    Wall,
    Plate,
    Door
}

public class Vector2I
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2I(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        if (obj is Vector2I other)
        {
            return X == other.X && Y == other.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

public class GridMap
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    private TileType[,] _grid;

    public GridMap(int width, int height)
    {
        Width = width;
        Height = height;
        _grid = new TileType[width, height];
        InitializeDefaultLayout();
    }

    private void InitializeDefaultLayout()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                {
                    _grid[x, y] = TileType.Wall;
                }
                else
                {
                    _grid[x, y] = TileType.Empty;
                }
            }
        }
    }

    public TileType GetTile(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return TileType.Wall;
        return _grid[x, y];
    }

    public void SetTile(int x, int y, TileType type)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            _grid[x, y] = type;
        }
    }

    public bool IsWalkable(int x, int y, bool isDoorOpen)
    {
        TileType tile = GetTile(x, y);
        if (tile == TileType.Wall) return false;
        if (tile == TileType.Door && !isDoorOpen) return false;
        return true;
    }
}