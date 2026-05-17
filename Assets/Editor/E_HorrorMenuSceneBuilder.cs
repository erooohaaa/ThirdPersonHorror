#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public static class E_HorrorMenuSceneBuilder
{
    private const string SceneFolder = "Assets/E_HorrorUI/Scenes";

    [MenuItem("Tools/E Horror UI/Create Main Menu and Settings Menu")]
    public static void CreateMenus()
    {
        Directory.CreateDirectory(SceneFolder);

        CreateMainMenu();
        CreateSettingsMenu();

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Done", "Created MainMenu and SettingsMenu in Assets/E_HorrorUI/Scenes", "OK");
    }

    private static void CreateMainMenu()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        GameObject camera = new GameObject("Main Camera");
        camera.tag = "MainCamera";
        Camera cam = camera.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.015f, 0.015f, 0.02f);

        GameObject canvas = CreateCanvas("MainMenuCanvas");
        E_MainMenuController menu = canvas.AddComponent<E_MainMenuController>();

        CreateText(canvas.transform, "TitleText", "THIRD PERSON HORROR", 52, new Vector2(0, 190), new Vector2(850, 80));
        CreateText(canvas.transform, "SubtitleText", "Dark Forest Cemetery", 26, new Vector2(0, 125), new Vector2(650, 50));

        Button start = CreateButton(canvas.transform, "StartButton", "START", new Vector2(0, 35));
        Button settings = CreateButton(canvas.transform, "SettingsButton", "SETTINGS", new Vector2(0, -45));
        Button quit = CreateButton(canvas.transform, "QuitButton", "QUIT", new Vector2(0, -125));

        start.onClick.AddListener(menu.StartGame);
        settings.onClick.AddListener(menu.OpenSettings);
        quit.onClick.AddListener(menu.QuitGame);

        EditorSceneManager.SaveScene(scene, SceneFolder + "/MainMenu.unity");
    }

    private static void CreateSettingsMenu()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        GameObject camera = new GameObject("Main Camera");
        camera.tag = "MainCamera";
        Camera cam = camera.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.015f, 0.015f, 0.02f);

        GameObject canvas = CreateCanvas("SettingsMenuCanvas");
        E_SettingsMenuController settings = canvas.AddComponent<E_SettingsMenuController>();

        CreateText(canvas.transform, "TitleText", "SETTINGS", 48, new Vector2(0, 210), new Vector2(700, 70));

        Text volumeLabel = CreateText(canvas.transform, "VolumeLabel", "Volume", 26, new Vector2(-220, 120), new Vector2(180, 40));
        volumeLabel.alignment = TextAnchor.MiddleLeft;

        Slider volume = CreateSlider(canvas.transform, "VolumeSlider", new Vector2(80, 120));
        volume.onValueChanged.AddListener(settings.SetVolume);
        settings.masterVolumeSlider = volume;

        Toggle shadows = CreateToggle(canvas.transform, "ShadowsToggle", "Enable Shadows", new Vector2(0, 45));
        shadows.onValueChanged.AddListener(settings.SetShadows);
        settings.shadowsToggle = shadows;

        Text keys = CreateText(canvas.transform, "KeyMappingText", "", 20, new Vector2(0, -70), new Vector2(560, 180));
        settings.keyMappingText = keys;

        Button back = CreateButton(canvas.transform, "BackButton", "BACK", new Vector2(0, -220));
        back.onClick.AddListener(settings.BackToMainMenu);

        EditorSceneManager.SaveScene(scene, SceneFolder + "/SettingsMenu.unity");
    }

    private static GameObject CreateCanvas(string name)
    {
        GameObject canvasGO = new GameObject(name);
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasGO.AddComponent<GraphicRaycaster>();

        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
        eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();

        GameObject bg = new GameObject("DarkBackground");
        bg.transform.SetParent(canvasGO.transform, false);
        Image image = bg.AddComponent<Image>();
        image.color = new Color(0.02f, 0.02f, 0.03f, 1f);
        RectTransform rect = bg.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        return canvasGO;
    }

    private static Text CreateText(Transform parent, string name, string value, int size, Vector2 pos, Vector2 dimensions)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        Text text = go.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.text = value;
        text.fontSize = size;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;

        RectTransform rect = go.GetComponent<RectTransform>();
        rect.sizeDelta = dimensions;
        rect.anchoredPosition = pos;

        return text;
    }

    private static Button CreateButton(Transform parent, string name, string label, Vector2 pos)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);

        Image image = go.AddComponent<Image>();
        image.color = new Color(0.13f, 0.02f, 0.03f, 0.95f);

        Button button = go.AddComponent<Button>();
        ColorBlock colors = button.colors;
        colors.normalColor = new Color(0.13f, 0.02f, 0.03f, 0.95f);
        colors.highlightedColor = new Color(0.34f, 0.03f, 0.05f, 1f);
        colors.pressedColor = new Color(0.55f, 0.02f, 0.04f, 1f);
        button.colors = colors;

        RectTransform rect = go.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(340, 64);
        rect.anchoredPosition = pos;

        CreateText(go.transform, "Text", label, 26, Vector2.zero, new Vector2(340, 64));
        return button;
    }

    private static Slider CreateSlider(Transform parent, string name, Vector2 pos)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        RectTransform rect = go.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(360, 30);
        rect.anchoredPosition = pos;

        Slider slider = go.AddComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 1f;

        GameObject bg = new GameObject("Background");
        bg.transform.SetParent(go.transform, false);
        Image bgImg = bg.AddComponent<Image>();
        bgImg.color = new Color(0.25f, 0.25f, 0.25f);
        RectTransform bgRect = bg.GetComponent<RectTransform>();
        bgRect.anchorMin = new Vector2(0, 0.25f);
        bgRect.anchorMax = new Vector2(1, 0.75f);
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;

        GameObject fill = new GameObject("Fill");
        fill.transform.SetParent(go.transform, false);
        Image fillImg = fill.AddComponent<Image>();
        fillImg.color = new Color(0.7f, 0.02f, 0.05f);
        RectTransform fillRect = fill.GetComponent<RectTransform>();
        fillRect.anchorMin = new Vector2(0, 0.25f);
        fillRect.anchorMax = new Vector2(1, 0.75f);
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;

        GameObject handle = new GameObject("Handle");
        handle.transform.SetParent(go.transform, false);
        Image handleImg = handle.AddComponent<Image>();
        handleImg.color = Color.white;
        RectTransform handleRect = handle.GetComponent<RectTransform>();
        handleRect.sizeDelta = new Vector2(26, 26);

        slider.fillRect = fillRect;
        slider.handleRect = handleRect;
        slider.targetGraphic = handleImg;

        return slider;
    }

    private static Toggle CreateToggle(Transform parent, string name, string label, Vector2 pos)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        RectTransform rect = go.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(420, 42);
        rect.anchoredPosition = pos;

        Toggle toggle = go.AddComponent<Toggle>();

        GameObject box = new GameObject("Box");
        box.transform.SetParent(go.transform, false);
        Image boxImg = box.AddComponent<Image>();
        boxImg.color = Color.white;
        RectTransform boxRect = box.GetComponent<RectTransform>();
        boxRect.sizeDelta = new Vector2(30, 30);
        boxRect.anchoredPosition = new Vector2(-170, 0);

        GameObject check = new GameObject("Checkmark");
        check.transform.SetParent(box.transform, false);
        Image checkImg = check.AddComponent<Image>();
        checkImg.color = new Color(0.7f, 0.02f, 0.05f);
        RectTransform checkRect = check.GetComponent<RectTransform>();
        checkRect.anchorMin = new Vector2(0.2f, 0.2f);
        checkRect.anchorMax = new Vector2(0.8f, 0.8f);
        checkRect.offsetMin = Vector2.zero;
        checkRect.offsetMax = Vector2.zero;

        Text text = CreateText(go.transform, "Label", label, 24, new Vector2(40, 0), new Vector2(320, 42));
        text.alignment = TextAnchor.MiddleLeft;

        toggle.targetGraphic = boxImg;
        toggle.graphic = checkImg;
        toggle.isOn = true;

        return toggle;
    }
}
#endif
