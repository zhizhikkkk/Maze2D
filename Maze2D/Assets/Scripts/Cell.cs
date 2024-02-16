using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject WallLeft;
    public GameObject WallBottom;
    public GameObject WallTop;
    public GameObject WallRight;

    public void SetWalls(bool left, bool right, bool top, bool bottom)
    {
        WallLeft.SetActive(left);
        WallRight.SetActive(right);
        WallTop.SetActive(top);
        WallBottom.SetActive(bottom);
    }
    
}
