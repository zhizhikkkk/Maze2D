using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Синглтон для удобного доступа к менеджеру

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
            Debug.Log("Вы проиграли! Время вышло.");
            EndGame();
        }
    }

    public void EndGame()
    {
        gameHasEnded = true;
        // Здесь код для отображения результатов и/или перезапуска
    }

    public void PlayerWon()
    {
        Debug.Log("Вы выиграли!");
        gameHasEnded = true;
        // Здесь код для сохранения результатов и/или перезапуска
    }
    public void RestartGame()
    {
        // Найти игрока и сбросить его позицию
        PlayerControls playerControls = FindObjectOfType<PlayerControls>();
        if (playerControls != null)
        {
            playerControls.ResetPlayer();
        }

        // Вызовите любые другие методы, которые нужны для рестарта, например, сгенерировать новый лабиринт
        MazeSpawner mazeSpawner = FindObjectOfType<MazeSpawner>();
        if (mazeSpawner != null)
        {
            mazeSpawner.Generate();
        }
    }
}
