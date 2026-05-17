using UnityEngine;
using TMPro;

public class Level2Manager : MonoBehaviour
{
    public static Level2Manager Instance;

    [Header("Level 2 Settings")]
    public int requiredPages = 3;
    public int scorePerPage = 10;

    [Header("Current Progress")]
    public int collectedPages = 0;
    public int score = 0;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text objectiveText;

    [Header("Gate")]
    public CemeteryGate cemeteryGate;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip collectSound;
    public AudioClip gateOpenSound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();

        if (cemeteryGate != null)
        {
            cemeteryGate.LockGate();
        }
    }

    public void CollectPage()
    {
        collectedPages++;
        score += scorePerPage;

        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        UpdateUI();

        if (collectedPages >= requiredPages)
        {
            OpenGate();
        }
    }

    private void OpenGate()
    {
        if (cemeteryGate != null)
        {
            cemeteryGate.OpenGate();
        }

        if (audioSource != null && gateOpenSound != null)
        {
            audioSource.PlayOneShot(gateOpenSound);
        }

        if (objectiveText != null)
        {
            objectiveText.text = "Gate opened! Go to the exit.";
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (objectiveText != null)
        {
            objectiveText.text = "Collect cursed pages: " + collectedPages + " / " + requiredPages;
        }
    }
}
