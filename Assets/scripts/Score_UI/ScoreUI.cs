using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool showHighScore = true;
    int lastShown = -1, lastHigh = -1;

    void Update()
    {
        if (ScoreManager.Instance == null || scoreText == null) return;
        int cur = ScoreManager.Instance.CurrentScore;
        int hi  = ScoreManager.Instance.HighScore;
        if (cur != lastShown || hi != lastHigh)
        {
            scoreText.text = showHighScore ? $"SCORE {cur:0000}   HI {hi:0000}"
                                           : $"SCORE {cur:0000}";
            lastShown = cur; lastHigh = hi;
        }
    }
}

