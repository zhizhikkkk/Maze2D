using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBottom = true;

    public bool Visited = false;
    public int DistanceFromStart;
}
public class MazeGenerator
{
    public int Width = 20;
    public int Height = 14;
    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[Width, Height];

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                maze[i, j] = new MazeGeneratorCell { X = i, Y = j };
            }
        }
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            maze[i, Height - 1].WallLeft = false;
        }
        for (int j = 0; j < maze.GetLength(1); j++)
        {
            maze[Width - 1, j].WallBottom = false;
        }

        RemoveWallsWithBacktracer(maze);

        PlaceMazeExit(maze);
        return maze;
    }



    private void RemoveWallsWithBacktracer(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.Visited = true;
        current.DistanceFromStart = 0;
        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbors = new List<MazeGeneratorCell>();
            int x = current.X;
            int y = current.Y;
            if (x > 0 && !maze[x - 1, y].Visited)
            {
                unvisitedNeighbors.Add(maze[x - 1, y]);
            }
            if (y > 0 && !maze[x, y - 1].Visited)
            {
                unvisitedNeighbors.Add(maze[x, y - 1]);
            }
            if (x < Width - 2 && !maze[x + 1, y].Visited)
            {
                unvisitedNeighbors.Add(maze[x + 1, y]);
            }
            if (y < Height - 2 && !maze[x, y + 1].Visited)
            {
                unvisitedNeighbors.Add(maze[x, y + 1]);
            }

            if (unvisitedNeighbors.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];
                RemoveWall(current, chosen);
                chosen.Visited = true;
                stack.Push(chosen);
                current = chosen;
                chosen.DistanceFromStart = stack.Count;

            }
            else
            {
                current = stack.Pop();
            }

        }
        while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y)
            {
                a.WallBottom = false;
            }
            else
            {
                b.WallBottom = false;
            }
        }
        else
        {
            if (a.X > b.X)
            {
                a.WallLeft = false;
            }
            else
            {
                b.WallLeft = false;
            }
        }
    }
    private void PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell furthest = maze[0, 0];

        for (int i = 0; i < maze.GetLength(0); i++)
        {

            if (maze[i, Height - 2].DistanceFromStart > furthest.DistanceFromStart)
            {
                furthest = maze[i, Height - 2];
            }
            if (maze[i, 0].DistanceFromStart > furthest.DistanceFromStart)
            {
                furthest = maze[i, 0];
            }

        }
        for (int j = 0; j < maze.GetLength(1); j++)
        {
            if (maze[Width-2,j].DistanceFromStart > furthest.DistanceFromStart)
            {
                furthest = maze[Width - 2, j];
            }
            if (maze[0,j].DistanceFromStart> furthest.DistanceFromStart)
            {
                furthest = maze[0,j];
            }
        }

        if (furthest.X == 0)
        {
            furthest.WallLeft = false;
        }
        else if(furthest.Y == 0)
        {
            furthest.WallBottom = false;
        }
        else if(furthest.X == Width - 2)
        {
            maze[furthest.X+1, furthest.Y].WallLeft = false;
        }
        else if(furthest.Y == Height - 2)
        {
            maze[furthest.X, furthest.Y+1].WallBottom = false;
        }

    }
}
