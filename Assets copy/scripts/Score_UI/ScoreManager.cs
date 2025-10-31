using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; } = 0;
    public int HighScore { get; private set; } = 0;

    public event Action<int,int> OnScoreChanged; // (current, high)

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        HighScore = PlayerPrefs.GetInt("HIGH_SCORE", 0);
    }

    public void Add(int points)
    {
        CurrentScore += points;
        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            PlayerPrefs.SetInt("HIGH_SCORE", HighScore);
        }
        OnScoreChanged?.Invoke(CurrentScore, HighScore);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        OnScoreChanged?.Invoke(CurrentScore, HighScore);
    }
}
