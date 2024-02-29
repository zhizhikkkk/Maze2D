using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;

    public UnityEvent<string, float> submitScoreEvent;
    private float _score;
    public LeaderBoard leaderBoard;
    public void SetScore(float score)
    {
        _score = (float)Math.Round(score, 1);
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
    public float Score;
}