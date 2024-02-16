using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;
    public bool WallLeft = true;
    public bool WallRight = true;
    public bool WallTop = true;
    public bool WallBottom = true;
    public bool Visited = false;
    public bool IsPassable; // Определяет, является ли ячейка проходимой
    public int DistanceFromStart;
}
public class MazeGenerator
{
    public int Radius = 10; // Радиус окружности лабиринта
    private GameObject mazeParent;

    public MazeGeneratorCell[,] GenerateMaze(GameObject parent)
    {
        mazeParent = parent;
        CalculateMazeSize();
        int diameter = Radius * 2;
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[diameter, diameter];
        Vector2 center = new Vector2(Radius, Radius);

        // Инициализация лабиринта
        for (int i = 0; i < diameter; i++)
        {
            for (int j = 0; j < diameter; j++)
            {
                float distance = Vector2.Distance(new Vector2(i, j), center);
                if (distance < Radius)
                {
                    maze[i, j] = new MazeGeneratorCell { X = i, Y = j, IsPassable = true };
                }
                else
                {
                    maze[i, j] = new MazeGeneratorCell { X = i, Y = j, IsPassable = false };
                }
            }
        }

        // Генерация лабиринта
        RemoveWallsWithBacktracker(maze, center);
        PlaceMazeExit(maze);
        return maze;
    }
    private void CalculateMazeSize()
    {
        // Получаем размеры экрана
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // Вычисляем радиус на основе размеров экрана, возможно, вам потребуется умножить на некоторый коэффициент
        Radius = (int)(Mathf.Min(screenWidth, screenHeight) / 2 * 0.9f);
    }
    private void PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        // Выберем крайнюю ячейку в крайней строке или столбце, которая проходима.
        MazeGeneratorCell exitCell = null;

        // Проверяем верхнюю и нижнюю строки
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            if (maze[i, 0].IsPassable)
            {
                exitCell = maze[i, 0];
            }
            if (maze[i, maze.GetLength(1) - 1].IsPassable)
            {
                exitCell = maze[i, maze.GetLength(1) - 1];
            }
        }

        // Проверяем левый и правый столбцы
        for (int j = 0; j < maze.GetLength(1); j++)
        {
            if (maze[0, j].IsPassable)
            {
                exitCell = maze[0, j];
            }
            if (maze[maze.GetLength(0) - 1, j].IsPassable)
            {
                exitCell = maze[maze.GetLength(0) - 1, j];
            }
        }
        if (exitCell != null)
        {
            if (exitCell.X == 0)
            {
                exitCell.WallLeft = false;
            }
            else if (exitCell.Y == 0)
            {
                exitCell.WallBottom = false;
            }
            else if (exitCell.X == maze.GetLength(0) - 1)
            {
                exitCell.WallRight = false;
            }
            else if (exitCell.Y == maze.GetLength(1) - 1)
            {
                exitCell.WallTop = false;
            }
        }

    }
    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze, Vector2 center)
    {

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        MazeGeneratorCell startCell = maze[(int)center.x, (int)center.y];
        startCell.Visited = true;
        startCell.DistanceFromStart = 0;
        stack.Push(startCell);

        while (stack.Count > 0)
        {
            MazeGeneratorCell currentCell = stack.Peek();
            MazeGeneratorCell nextCell = GetNextCell(maze, currentCell);

            if (nextCell != null)
            {
                RemoveWall(currentCell, nextCell);
                nextCell.Visited = true;
                stack.Push(nextCell);
                nextCell.DistanceFromStart = stack.Count;
            }
            else
            {
                stack.Pop();
            }
        }
    }

    private MazeGeneratorCell GetNextCell(MazeGeneratorCell[,] maze, MazeGeneratorCell currentCell)
    {
        List<MazeGeneratorCell> unvisitedNeighbors = new List<MazeGeneratorCell>();

        // Проверка соседей
        int[][] directions = new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { -1, 0 } };
        foreach (var dir in directions)
        {
            int newX = currentCell.X + dir[0];
            int newY = currentCell.Y + dir[1];
            if (newX >= 0 && newX < maze.GetLength(0) && newY >= 0 && newY < maze.GetLength(1) && maze[newX, newY].IsPassable && !maze[newX, newY].Visited)
            {
                unvisitedNeighbors.Add(maze[newX, newY]);
            }
        }

        if (unvisitedNeighbors.Count > 0)
        {
            int randIndex = Random.Range(0, unvisitedNeighbors.Count);
            return unvisitedNeighbors[randIndex];
        }
        return null;
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        // Определение положения b относительно a и удаление соответствующих стен
        if (a.X == b.X)
        {
            if (a.Y > b.Y)
            {
                a.WallBottom = false;
                b.WallTop = false; // Убедитесь, что у вас есть соответствующее свойство в MazeGeneratorCell
            }
            else
            {
                a.WallTop = false;
                b.WallBottom = false;
            }
        }
        else if (a.Y == b.Y)
        {
            if (a.X > b.X)
            {
                a.WallLeft = false;
                b.WallRight = false;
            }
            else
            {
                a.WallRight = false;
                b.WallLeft = false;
            }
        }
    }
}
#region RectangleMaze
//public class MazeGeneratorCell
//{
//    public int X;
//    public int Y;

//    public bool WallLeft = true;
//    public bool WallBottom = true;

//    public bool Visited = false;
//    public int DistanceFromStart;
//}
//public class MazeGenerator
//{
//    public int Width = 20;
//    public int Height = 14;
//    public MazeGeneratorCell[,] GenerateMaze()
//    {
//        MazeGeneratorCell[,] maze = new MazeGeneratorCell[Width, Height];

//        for (int i = 0; i < maze.GetLength(0); i++)
//        {
//            for (int j = 0; j < maze.GetLength(1); j++)
//            {
//                maze[i, j] = new MazeGeneratorCell { X = i, Y = j };
//            }
//        }
//        for (int i = 0; i < maze.GetLength(0); i++)
//        {
//            maze[i, Height - 1].WallLeft = false;
//        }
//        for (int j = 0; j < maze.GetLength(1); j++)
//        {
//            maze[Width - 1, j].WallBottom = false;
//        }

//        RemoveWallsWithBacktracer(maze);

//        PlaceMazeExit(maze);
//        return maze;
//    }

//    private void RemoveWallsWithBacktracer(MazeGeneratorCell[,] maze)
//    {
//        MazeGeneratorCell current = maze[0, 0];
//        current.Visited = true;
//        current.DistanceFromStart = 0;
//        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
//        do
//        {
//            List<MazeGeneratorCell> unvisitedNeighbors = new List<MazeGeneratorCell>();
//            int x = current.X;
//            int y = current.Y;
//            if (x > 0 && !maze[x - 1, y].Visited)
//            {
//                unvisitedNeighbors.Add(maze[x - 1, y]);
//            }
//            if (y > 0 && !maze[x, y - 1].Visited)
//            {
//                unvisitedNeighbors.Add(maze[x, y - 1]);
//            }
//            if (x < Width - 2 && !maze[x + 1, y].Visited)
//            {
//                unvisitedNeighbors.Add(maze[x + 1, y]);
//            }
//            if (y < Height - 2 && !maze[x, y + 1].Visited)
//            {
//                unvisitedNeighbors.Add(maze[x, y + 1]);
//            }

//            if (unvisitedNeighbors.Count > 0)
//            {
//                MazeGeneratorCell chosen = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];
//                RemoveWall(current, chosen);
//                chosen.Visited = true;
//                stack.Push(chosen);
//                current = chosen;
//                chosen.DistanceFromStart = stack.Count;

//            }
//            else
//            {
//                current = stack.Pop();
//            }

//        }
//        while (stack.Count > 0);
//    }

//    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
//    {
//        if (a.X == b.X)
//        {
//            if (a.Y > b.Y)
//            {
//                a.WallBottom = false;
//            }
//            else
//            {
//                b.WallBottom = false;
//            }
//        }
//        else
//        {
//            if (a.X > b.X)
//            {
//                a.WallLeft = false;
//            }
//            else
//            {
//                b.WallLeft = false;
//            }
//        }
//    }
//    private void PlaceMazeExit(MazeGeneratorCell[,] maze)
//    {
//        MazeGeneratorCell furthest = maze[0, 0];

//        for (int i = 0; i < maze.GetLength(0); i++)
//        {

//            if (maze[i, Height - 2].DistanceFromStart > furthest.DistanceFromStart)
//            {
//                furthest = maze[i, Height - 2];
//            }
//            if (maze[i, 0].DistanceFromStart > furthest.DistanceFromStart)
//            {
//                furthest = maze[i, 0];
//            }

//        }
//        for (int j = 0; j < maze.GetLength(1); j++)
//        {
//            if (maze[Width-2,j].DistanceFromStart > furthest.DistanceFromStart)
//            {
//                furthest = maze[Width - 2, j];
//            }
//            if (maze[0,j].DistanceFromStart> furthest.DistanceFromStart)
//            {
//                furthest = maze[0,j];
//            }
//        }

//        if (furthest.X == 0)
//        {
//            furthest.WallLeft = false;
//        }
//        else if(furthest.Y == 0)
//        {
//            furthest.WallBottom = false;
//        }
//        else if(furthest.X == Width - 2)
//        {
//            maze[furthest.X+1, furthest.Y].WallLeft = false;
//        }
//        else if(furthest.Y == Height - 2)
//        {
//            maze[furthest.X, furthest.Y+1].WallBottom = false;
//        }

//    }
//}
#endregion
