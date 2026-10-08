using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void AddScore(int score)
    {
        scoreText.text = score.ToString("00000");
    }
}
