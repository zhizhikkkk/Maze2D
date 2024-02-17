using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // �������� ��� �������� ������� � ���������

    public float timeRemaining = 60f;
    public bool gameHasEnded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (gameHasEnded)
        {
            return;
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            Debug.Log("�� ���������! ����� �����.");
            EndGame();
        }
    }

    public void EndGame()
    {
        gameHasEnded = true;
        // ����� ��� ��� ����������� ����������� �/��� �����������
    }

    public void PlayerWon()
    {
        Debug.Log("�� ��������!");
        gameHasEnded = true;
        // ����� ��� ��� ���������� ����������� �/��� �����������
    }
    public void RestartGame()
    {
        // ����� ������ � �������� ��� �������
        PlayerControls playerControls = FindObjectOfType<PlayerControls>();
        if (playerControls != null)
        {
            playerControls.ResetPlayer();
        }

        // �������� ����� ������ ������, ������� ����� ��� ��������, ��������, ������������� ����� ��������
        MazeSpawner mazeSpawner = FindObjectOfType<MazeSpawner>();
        if (mazeSpawner != null)
        {
            mazeSpawner.Generate();
        }
    }
}
