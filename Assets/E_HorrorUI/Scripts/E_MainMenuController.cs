using UnityEngine;
using UnityEngine.SceneManagement;

public class E_MainMenuController : MonoBehaviour
{
    public string firstLevelSceneName = "Level1_AbandonedHouse";
    public string settingsSceneName = "SettingsMenu";

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(firstLevelSceneName);
    }

    public void OpenSettings()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(settingsSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
