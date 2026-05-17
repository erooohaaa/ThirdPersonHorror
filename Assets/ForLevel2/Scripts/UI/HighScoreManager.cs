using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TMP_Text highScoreText;

    public void SaveHighScore(int score)
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }

        UpdateHighScoreText();
    }

    public void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        UpdateHighScoreText();
    }

    private void Start()
    {
        UpdateHighScoreText();
    }
}
