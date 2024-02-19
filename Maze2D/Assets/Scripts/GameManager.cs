using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Синглтон для удобного доступа к менеджеру

    public float timeRemaining = 60f;
    public bool gameHasEnded = false;
    public GameObject winPanel;
    public GameObject losePanel;
    public string currentPlayerName;
    public PlayerControls player;
    public TextMeshProUGUI winPanelScoreText;
    public MazeSpawner spawner;
    public GameObject startPanel;
    public GameObject leaderboardPanel;
    public ScoreManager scoreManager;
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

    public void StartGame()
    {
        Time.timeScale = 1f; // Возобновляем время в игре
        gameHasEnded = false;
        timeRemaining = 60f; 
    }
    public void EndGame()
    {
        gameHasEnded = true;
        // Здесь код для отображения результатов и/или перезапуска
        losePanel.SetActive(true);
    }

    public void PlayerWon()
    {
        Debug.Log("Вы выиграли!");
        float score = 60 - timeRemaining;
        winPanelScoreText.text = "Your score :" + score;
        gameHasEnded = true;
        winPanel.SetActive(true);
        scoreManager.SetScore((int)score);
        scoreManager.SubmitScore();
    }
    
    public void RestartGame()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        startPanel.SetActive(true) ;
    }
    public void RestartGame2()
    {
        startPanel.SetActive(false) ;
        leaderboardPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        spawner.Generate();
        StartGame();
        player.ResetPosition();
    }
    public void OpenLeaderBoard()
    {
        leaderboardPanel.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        startPanel.SetActive(false);
    }
}
