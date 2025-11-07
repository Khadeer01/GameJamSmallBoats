using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int currentScore = 0;

    public int CurrentScore => currentScore;

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

    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log($"Score added: {amount}, Total Score: {currentScore}");

        ScoreUI scoreUI = FindFirstObjectByType<ScoreUI>();

        if (scoreUI != null)
        {
            scoreUI.UpdateScoreText(currentScore);
        }
    }

     public void ResetScore()
    {
        currentScore = 0;
        ScoreUI scoreUI = FindFirstObjectByType<ScoreUI>();
        if (scoreUI != null)
        {
            scoreUI.UpdateScoreText(currentScore);
        }
    }
}
