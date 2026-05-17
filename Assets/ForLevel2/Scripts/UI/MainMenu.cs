using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevelSceneName = "Level1_AbandonedHouse";
    public string settingsSceneName = "SettingsMenu";

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelSceneName);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(settingsSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
