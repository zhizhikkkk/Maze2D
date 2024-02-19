using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;
    private int _score;
    public LeaderBoard leaderBoard;
    public void SetScore(int score)
    {
        _score = score;
    }

    public void SubmitScore()
    {
        var newScore = new ScoreEntry { Name = inputName.text, Score = _score };
        leaderBoard.AddScoreEntry(newScore);
    }
    public void ChangeName(string name)
    {
        inputName.text = name;
    }
}
[Serializable]
public class ScoreEntry
{
    public string Name;
    public int Score;
}