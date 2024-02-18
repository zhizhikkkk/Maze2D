using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UIElements;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public float score;
}

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    public List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
    public GameObject leaderboardPanel;
    public GameObject nameInputPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public InputField nameInputField;
    public Text leaderboardText;

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

    public void StartGame()
    {
        nameInputPanel.SetActive(true);
        string playerName = nameInputField.text;
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            GameManager.Instance.currentPlayerName = playerName;
            nameInputPanel.SetActive(false);// Замените "GameScene" на имя вашей игровой сцены
        }
        else
        {
            Debug.LogError("Player name is empty!");
        }
    }

    public void AddScore(float score)
    {
        leaderboardEntries.Add(new LeaderboardEntry { playerName = GameManager.Instance.currentPlayerName, score = score });
        leaderboardEntries = leaderboardEntries.OrderBy(entry => entry.score).ToList();
        if (leaderboardEntries.Count > 10)
        {
            leaderboardEntries.RemoveRange(10, leaderboardEntries.Count - 10);
        }

        UpdateLeaderboardUI();
    }

    private void UpdateLeaderboardUI()
    {
        leaderboardText.text = "Leaderboard:\n";
        foreach (var entry in leaderboardEntries)
        {
            leaderboardText.text += $"{entry.playerName} - {entry.score}\n";
        }
        leaderboardPanel.SetActive(true);
    }
    public void AskForPlayerName() // Вызывается в начале и после перезапуска игры
    {
        nameInputPanel.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        leaderboardPanel.SetActive(false);
    }

    // Возможно, вам понадобится метод для открытия панели таблицы лидеров
    public void ShowLeaderboard()
    {
        leaderboardPanel.SetActive(true);
        UpdateLeaderboardUI();
    }
}
