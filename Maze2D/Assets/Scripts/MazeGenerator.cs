using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

    public MazeGeneratorCell[,] GenerateMaze(GameObject parent, out MazeGeneratorCell exitCell)
    {
        mazeParent = parent;
        //CalculateMazeSize();
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
        exitCell = PlaceMazeExit(maze);
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
 
    //private MazeGeneratorCell PlaceMazeExit(MazeGeneratorCell[,] maze)
    //{
    //    List<MazeGeneratorCell> edgeCells = new List<MazeGeneratorCell>();

    //    // Собираем все проходимые ячейки по периметру
    //    for (int i = 0; i < maze.GetLength(0); i++)
    //    {
    //        if (maze[i, 0].IsPassable) edgeCells.Add(maze[i, 0]);
    //        if (maze[i, maze.GetLength(1) - 1].IsPassable) edgeCells.Add(maze[i, maze.GetLength(1) - 1]);
    //    }
    //    for (int j = 0; j < maze.GetLength(1); j++)
    //    {
    //        if (maze[0, j].IsPassable) edgeCells.Add(maze[0, j]);
    //        if (maze[maze.GetLength(0) - 1, j].IsPassable) edgeCells.Add(maze[maze.GetLength(0) - 1, j]);
    //    }

    //    MazeGeneratorCell exitCell = null;
    //    // Выбираем случайную ячейку из списка крайних ячеек
    //    if (edgeCells.Count > 0)
    //    {
    //        exitCell = edgeCells[Random.Range(0, edgeCells.Count)];
    //        // Удаляем стену, чтобы создать выход
    //        if (exitCell.X == 0)
    //        {
    //            exitCell.WallLeft = false;
    //        }
    //        else if (exitCell.Y == 0)
    //        {
    //            exitCell.WallBottom = false;
    //        }
    //        else if (exitCell.X == maze.GetLength(0) - 1)
    //        {
    //            exitCell.WallRight = false;
    //        }
    //        else if (exitCell.Y == maze.GetLength(1) - 1)
    //        {
    //            exitCell.WallTop = false;
    //        }


    //    }
    //    return exitCell;
    //}
    private MazeGeneratorCell PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        List<MazeGeneratorCell> edgeCells = new List<MazeGeneratorCell>();

        // Собираем все проходимые ячейки по периметру
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            if (maze[i, 0].IsPassable) edgeCells.Add(maze[i, 0]);
            if (maze[i, maze.GetLength(1) - 1].IsPassable) edgeCells.Add(maze[i, maze.GetLength(1) - 1]);
        }
        for (int j = 0; j < maze.GetLength(1); j++)
        {
            if (maze[0, j].IsPassable) edgeCells.Add(maze[0, j]);
            if (maze[maze.GetLength(0) - 1, j].IsPassable) edgeCells.Add(maze[maze.GetLength(0) - 1, j]);
        }

        // Фильтруем edgeCells, чтобы оставить только те, которые ближе к предпочитаемому "логическому" краю
        // Это может быть реализовано различными способами, в зависимости от вашего предпочтения
        // Например, если вы хотите, чтобы выход был всегда на правой стороне, вы можете сделать так:
        edgeCells = edgeCells.Where(cell => cell.X == maze.GetLength(0) - 1).ToList();

        MazeGeneratorCell exitCell = null;
        if (edgeCells.Count > 0)
        {
            // Выбираем случайную ячейку из отфильтрованного списка
            exitCell = edgeCells[Random.Range(0, edgeCells.Count)];
            // Удаляем стену, чтобы создать выход
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
        return exitCell;
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

