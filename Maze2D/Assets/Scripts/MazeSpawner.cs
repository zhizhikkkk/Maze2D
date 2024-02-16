using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject CellPrefab;
    public GameObject MazeParent;
    

    private void Start()
    {
        Generate();
    }
    public void Generate()
    {
        ClearMaze();
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze(MazeParent);

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (maze[i, j].IsPassable) // ”бедитесь, что €чейка проходима
                {
                    GameObject cellObject = Instantiate(CellPrefab, new Vector3(i - generator.Radius, j - generator.Radius, 0), Quaternion.identity, MazeParent.transform);
                    Cell c = cellObject.GetComponent<Cell>();
                    c.SetWalls(maze[i, j].WallLeft, maze[i, j].WallRight, maze[i, j].WallTop, maze[i, j].WallBottom);
                }
            }
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

}
