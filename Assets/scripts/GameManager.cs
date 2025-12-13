using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    private bool gameOver = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (gameOverUI)
            gameOverUI.SetActive(false);
    }

    public void PlayerDied()
    {
        if (gameOver)
            return;

        gameOver = true;
        GameOver();
    }

    void GameOver()
    {
        Time.timeScale = 0f;

        if (gameOverUI)
            gameOverUI.SetActive(true);

        // 🔑 Save score to high score list ONCE
        if (ScoreManager.Instance != null)
        {
            int finalScore = ScoreManager.Instance.CurrentScore;
            FindObjectOfType<HighScoreManager>()?.AddScore(finalScore);
            ScoreManager.Instance.ResetScore();
        }
    }
}



