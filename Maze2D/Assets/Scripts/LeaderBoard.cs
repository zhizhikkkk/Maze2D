using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using System.Linq;
using System;


public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private List<ScoreEntry> scoreEntries = new List<ScoreEntry>();

    private void Start()
    {
        LoadScores();
        DisplayScores();
    }

    public void AddScoreEntry(ScoreEntry newScore)
    {
        LoadScores(); // �������� ������������ �����������
        scoreEntries.Add(newScore); // ���������� ������ ����������
        scoreEntries = scoreEntries.OrderBy(entry => entry.Score).ToList(); // ���������� �� �����������
        SaveScores(); // ���������� ������������ ������
        DisplayScores(); // ���������� �����������
    }

    private void LoadScores()
    {
        string json = PlayerPrefs.GetString("leaderboard", "");
        if (!string.IsNullOrEmpty(json))
        {
            scoreEntries = JsonUtility.FromJson<ScoreList>(json).Entries;
        }
    }

    private void SaveScores()
    {
        var list = new ScoreList { Entries = scoreEntries };
        string json = JsonUtility.ToJson(list);
        PlayerPrefs.SetString("leaderboard", json);
        PlayerPrefs.Save();
    }

    private void DisplayScores()
    {
        for (int i = 0; i < names.Count; i++)
        {
            if (i < scoreEntries.Count)
            {
                names[i].text = scoreEntries[i].Name;
                scores[i].text = scoreEntries[i].Score.ToString();
            }
            else
            {
                // ������� ������, ���� ��� ������ ��� �����������
                names[i].text = "";
                scores[i].text = "";
            }
        }
    }
    public void ClearLeaderboard()
    {
        PlayerPrefs.DeleteKey("leaderboard"); // ������� ������ ������ ����������
        scoreEntries.Clear(); // ������� ������� ������ �����������
        DisplayScores(); // ��������� ����������� ����������
    }
    public void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll(); // ������� ��� ������, ����������� ����� PlayerPrefs
        scoreEntries.Clear(); // ������� ������� ������ �����������
        DisplayScores(); // ��������� ����������� ����������
    }
}
[Serializable]
public class ScoreList
{
    public List<ScoreEntry> Entries = new List<ScoreEntry>();
}