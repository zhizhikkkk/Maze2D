using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // �������� ��� �������� ������� � ���������

    public float timeRemaining = 60f;
    public bool gameHasEnded = false;
    public GameObject winPanel;
    public GameObject losePanel;
    public string currentPlayerName;
    public PlayerControls player;
    public MazeSpawner spawner;
    public GameObject startPanel;
    public GameObject leaderboardPanel;
    public ScoreManager scoreManager;
    public TextMeshProUGUI currentScoreText;
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
            currentScoreText.text =( (int)timeRemaining).ToString();
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            Debug.Log("�� ���������! ����� �����.");
            EndGame();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f; // ������������ ����� � ����
        gameHasEnded = false;
        timeRemaining = 60f; 
    }
    public void EndGame()
    {
        gameHasEnded = true;
        // ����� ��� ��� ����������� ����������� �/��� �����������
        losePanel.SetActive(true);
        Time.timeScale = 1f; 
        timeRemaining = 60f;
    }

    public void PlayerWon()
    {
        Debug.Log("�� ��������!");
        float score = 60 - timeRemaining;
        gameHasEnded = true;
        winPanel.SetActive(true);
        scoreManager.SetScore(score);
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
        startPanel.SetActive(false);
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
    public void QuitGame()
    {
        Application.Quit();

    }
}
