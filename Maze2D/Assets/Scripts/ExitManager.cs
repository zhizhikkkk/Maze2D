using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public GameObject exitPrefab; // Убедитесь, что вы назначили префаб через инспектор

    public void CreateExit(MazeGeneratorCell exitCell, int Radius)
    {
        GameObject exitObject = Instantiate(
            exitPrefab,
            new Vector3((exitCell.X - Radius+0.5f)*0.5f, (exitCell.Y - Radius+0.5f)*0.5f, 0),
            Quaternion.identity
        );
        exitObject.tag = "Exit";
        exitObject.transform.parent = transform;
    }

}
