using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("ScoreUI is missing a TextMeshProuGUI reference!");
            return;
        }

        UpdateScoreText(GameManager.Instance.CurrentScore);
    }

    public void UpdateScoreText(int newScore)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {newScore}";
    }
}
