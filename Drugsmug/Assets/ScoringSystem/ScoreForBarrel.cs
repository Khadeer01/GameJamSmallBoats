using UnityEngine;

public class ScoreForBarrel : MonoBehaviour
{
    // Points for avoiding the barrel
    [SerializeField] private int scoreForAvoid = 25;
    // The point in which the barrel is off the screen (awards point)
    [SerializeField] private float offScreenY = -6f;

    private bool hasScored = false;

    // When the barrel falls, award score once
    private void Update()
    {
        if (!hasScored && transform.position.y < offScreenY)
        {
            hasScored = true;
            GameManager.Instance.AddScore(scoreForAvoid);
            Debug.Log($"Player avoided {gameObject.name}, +{scoreForAvoid} points awarded");
            Destroy(gameObject);
        }
    }

    // If it hits the player, don't award score
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasScored = true;
        }
    }
}
