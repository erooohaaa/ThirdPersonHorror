using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class E_SettingsMenuController : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";

    public Slider masterVolumeSlider;
    public Toggle shadowsToggle;
    public Text keyMappingText;

    private const string VolumeKey = "MasterVolume";
    private const string ShadowsKey = "ShadowsEnabled";

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        bool shadows = PlayerPrefs.GetInt(ShadowsKey, 1) == 1;

        if (masterVolumeSlider != null)
            masterVolumeSlider.value = volume;

        if (shadowsToggle != null)
            shadowsToggle.isOn = shadows;

        SetVolume(volume);
        SetShadows(shadows);
        UpdateKeyMappingText();
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }

    public void SetShadows(bool enabled)
    {
        QualitySettings.shadows = enabled ? ShadowQuality.All : ShadowQuality.Disable;
        PlayerPrefs.SetInt(ShadowsKey, enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void UpdateKeyMappingText()
    {
        if (keyMappingText == null)
            return;

        keyMappingText.text =
            "Key Mapping\n\n" +
            "Move: W A S D\n" +
            "Look: Mouse\n" +
            "Jump: Space\n" +
            "Run: Left Shift\n" +
            "Pause: Esc\n" +
            "Map View: M\n" +
            "Change Time: 1 / 2 / 3 / 4 / N";
    }
}
