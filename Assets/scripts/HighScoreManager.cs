using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts; // size 5

    const int MaxScores = 5;

    void Start()
    {
        LoadScores();
        DisplayScores();
    }

    public void AddScore(int newScore)
    {
        int[] scores = LoadScoresArray();

        scores[MaxScores - 1] = newScore;
        System.Array.Sort(scores);
        System.Array.Reverse(scores);

        for (int i = 0; i < MaxScores; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, scores[i]);
        }

        PlayerPrefs.Save();
    }

    void DisplayScores()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);
            scoreTexts[i].text = (i + 1) + ". " + score;
        }
    }

    int[] LoadScoresArray()
    {
        int[] scores = new int[MaxScores];
        for (int i = 0; i < MaxScores; i++)
        {
            scores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
        }
        return scores;
    }

    void LoadScores()
    {
        for (int i = 0; i < MaxScores; i++)
        {
            if (!PlayerPrefs.HasKey("HighScore" + i))
                PlayerPrefs.SetInt("HighScore" + i, 0);
        }
    }
}
