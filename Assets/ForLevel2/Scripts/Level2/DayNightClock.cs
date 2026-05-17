using UnityEngine;
using TMPro;

public class DayNightClock : MonoBehaviour
{
    [Header("Time Settings")]
    [Range(0f, 24f)]
    public float currentHour = 22f;

    [Tooltip("How many in-game minutes pass per real second.")]
    public float timeSpeed = 5f;

    public bool autoTime = true;

    [Header("Lighting")]
    public Light directionalLight;

    public Color morningColor = new Color(1f, 0.65f, 0.35f);
    public Color dayColor = new Color(1f, 0.95f, 0.8f);
    public Color eveningColor = new Color(1f, 0.35f, 0.25f);
    public Color nightColor = new Color(0.15f, 0.25f, 0.6f);

    public float dayIntensity = 1.2f;
    public float nightIntensity = 0.15f;

    [Header("Fog")]
    public bool controlFog = true;
    public Color morningFog = new Color(0.55f, 0.45f, 0.35f);
    public Color dayFog = new Color(0.55f, 0.65f, 0.75f);
    public Color eveningFog = new Color(0.35f, 0.25f, 0.25f);
    public Color nightFog = new Color(0.03f, 0.05f, 0.12f);

    [Header("Skybox")]
    public bool rotateSkybox = true;
    public float skyboxRotationSpeed = 1f;

    [Header("UI")]
    public TMP_Text timeText;

    private void Start()
    {
        ApplyTimeSettings();
    }

    private void Update()
    {
        if (autoTime)
        {
            currentHour += (timeSpeed / 60f) * Time.deltaTime;

            if (currentHour >= 24f)
            {
                currentHour = 0f;
            }

            ApplyTimeSettings();
        }

        HandleDebugKeys();
    }

    private void ApplyTimeSettings()
    {
        UpdateDirectionalLight();
        UpdateFog();
        UpdateSkybox();
        UpdateTimeUI();
    }

    private void UpdateDirectionalLight()
    {
        if (directionalLight == null)
        {
            return;
        }

        float sunAngle = (currentHour / 24f) * 360f - 90f;
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        directionalLight.color = GetTimeColor();

        float dayFactor = Mathf.Clamp01(Mathf.Sin((currentHour - 6f) / 12f * Mathf.PI));
        directionalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, dayFactor);
    }

    private void UpdateFog()
    {
        if (!controlFog)
        {
            return;
        }

        RenderSettings.fog = true;
        RenderSettings.fogColor = GetFogColor();
    }

    private void UpdateSkybox()
    {
        if (!rotateSkybox || RenderSettings.skybox == null)
        {
            return;
        }

        if (RenderSettings.skybox.HasProperty("_Rotation"))
        {
            float rotation = currentHour * 15f * skyboxRotationSpeed;
            RenderSettings.skybox.SetFloat("_Rotation", rotation);
        }
    }

    private void UpdateTimeUI()
    {
        if (timeText == null)
        {
            return;
        }

        int hours = Mathf.FloorToInt(currentHour);
        int minutes = Mathf.FloorToInt((currentHour - hours) * 60f);

        timeText.text = $"{hours:00}:{minutes:00}";
    }

    private Color GetTimeColor()
    {
        if (currentHour >= 5f && currentHour < 9f)
        {
            return morningColor;
        }

        if (currentHour >= 9f && currentHour < 17f)
        {
            return dayColor;
        }

        if (currentHour >= 17f && currentHour < 20f)
        {
            return eveningColor;
        }

        return nightColor;
    }

    private Color GetFogColor()
    {
        if (currentHour >= 5f && currentHour < 9f)
        {
            return morningFog;
        }

        if (currentHour >= 9f && currentHour < 17f)
        {
            return dayFog;
        }

        if (currentHour >= 17f && currentHour < 20f)
        {
            return eveningFog;
        }

        return nightFog;
    }

    private void HandleDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetMorning();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetDay();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetEvening();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetNight();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            GoToNextTimeStage();
        }
    }

    public void SetHour(float hour)
    {
        currentHour = Mathf.Clamp(hour, 0f, 24f);
        ApplyTimeSettings();
    }

    public void SetMorning()
    {
        SetHour(6f);
    }

    public void SetDay()
    {
        SetHour(12f);
    }

    public void SetEvening()
    {
        SetHour(18f);
    }

    public void SetNight()
    {
        SetHour(22f);
    }

    public void GoToNextTimeStage()
    {
        if (currentHour >= 20f || currentHour < 5f)
        {
            SetMorning();
        }
        else if (currentHour >= 5f && currentHour < 9f)
        {
            SetDay();
        }
        else if (currentHour >= 9f && currentHour < 17f)
        {
            SetEvening();
        }
        else
        {
            SetNight();
        }
    }
}
