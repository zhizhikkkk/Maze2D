using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject CellPrefab;
    public GameObject MazeParent;
    public float cellSize = 0.5f;

    private void Start()
    {
        Generate();
    }
    public void Generate()
    {
        ClearMaze();
        ClearExits();
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell exitCell;
        MazeGeneratorCell[,] maze = generator.GenerateMaze(MazeParent, out exitCell);






        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // ¬ычисл€ем размер €чейки, чтобы лабиринт поместилс€ на экране
        cellSize = Mathf.Min(screenWidth / (generator.Radius * 2.5f), screenHeight / (generator.Radius * 2.5f));




        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (maze[i, j].IsPassable) // ”бедитесь, что €чейка проходима
                {
                    //GameObject cellObject = Instantiate(CellPrefab, new Vector3(i - generator.Radius, j - generator.Radius, 0), Quaternion.identity, MazeParent.transform);
                    GameObject cellObject = Instantiate(CellPrefab, new Vector3((i - generator.Radius) * cellSize, (j - generator.Radius) * cellSize, 0), Quaternion.identity, MazeParent.transform);
                    Cell c = cellObject.GetComponent<Cell>();
                    c.SetWalls(maze[i, j].WallLeft, maze[i, j].WallRight, maze[i, j].WallTop, maze[i, j].WallBottom);
                    cellObject.transform.localScale = new Vector3(cellSize, cellSize, 1);
                }
            }
        }
        ExitManager exitManager = FindObjectOfType<ExitManager>();
        if (exitManager != null)
        {
            exitManager.CreateExit(exitCell,generator.Radius);
        }
        else
        {
            Debug.LogError("ExitManager not found on the scene!");
        }
    }

    public GameObject GetParent()
    {
        return MazeParent;
    }
    private void ClearMaze()
    {
        foreach (Transform child in MazeParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    private void ClearExits()
    {
        foreach (GameObject exit in GameObject.FindGameObjectsWithTag("Exit"))
        {
            Destroy(exit);
        }
    }
}
