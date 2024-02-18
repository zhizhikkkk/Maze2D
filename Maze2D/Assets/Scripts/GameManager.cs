using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // �������� ��� �������� ������� � ���������

    public float timeRemaining = 60f;
    public bool gameHasEnded = false;
    public GameObject winPanel;
    public GameObject losePanel;
    public string currentPlayerName;
    public PlayerControls player;
    public TextMeshProUGUI winPanelScoreText;
    public MazeSpawner spawner;
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
    }

    public void PlayerWon()
    {
        Debug.Log("�� ��������!");
        float score = 60 - timeRemaining;
        winPanelScoreText.text = "Your score :" + score;
        gameHasEnded = true;
        winPanel.SetActive(true);
    }
    
   
    public void RestartGame()
    {
        spawner.Generate();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        StartGame();
        player.ResetPosition();
    }
}
