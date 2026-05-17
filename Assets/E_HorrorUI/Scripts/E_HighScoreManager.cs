using UnityEngine;
using UnityEngine.UI;

public class E_HighScoreManager : MonoBehaviour
{
    public Text highScoreText;
    private const string HighScoreKey = "HighScore";

    private void Start()
    {
        UpdateHighScoreUI();
    }

    public void SaveScoreIfHigh(int score)
    {
        int oldHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (score > oldHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.Save();
        }

        UpdateHighScoreUI();
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(HighScoreKey);
        UpdateHighScoreUI();
    }

    public void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = "High Score: " + GetHighScore();
    }
}
