using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Toggle shadowsToggle;
    public string mainMenuSceneName = "MainMenu";

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        bool shadowsEnabled = PlayerPrefs.GetInt("ShadowsEnabled", 1) == 1;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }

        if (shadowsToggle != null)
        {
            shadowsToggle.isOn = shadowsEnabled;
        }

        SetVolume(savedVolume);
        SetShadows(shadowsEnabled);
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("MasterVolume", value);
        AudioListener.volume = value;

        if (audioMixer != null)
        {
            float db = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
            audioMixer.SetFloat("MasterVolume", db);
        }
    }

    public void SetShadows(bool enabled)
    {
        PlayerPrefs.SetInt("ShadowsEnabled", enabled ? 1 : 0);

        Light[] lights = FindObjectsByType<Light>(FindObjectsSortMode.None);
        foreach (Light light in lights)
        {
            light.shadows = enabled ? LightShadows.Soft : LightShadows.None;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
