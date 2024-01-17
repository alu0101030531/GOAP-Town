using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public delegate void SevenOClock();

public enum Day {
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset Preset;
    [Range(0, 24)] public float TimeOfDay;
    public TMP_Text timeUI;
    public TMP_Text dayUI;
    public static SevenOClock OnSevenOClock;
    private float timeThreshold = 1f;
    private float timeFactor = 0.3f;

    [Header("DayState")]
    private int days = 1;
    private Day currentDay;
    private int weekLength = 7;

    private void Start() {
        UpdateDaysUI();
        GoToWork.OnGetTime += GetTime;
        Citizens.OnGetTime += GetTime;
    }

    private void OnDestroy() {
        GoToWork.OnGetTime -= GetTime;
        Citizens.OnGetTime -= GetTime;
    }

    private void UpdateDaysUI() {
        dayUI.text = "Day: " + currentDay.ToString("g");
    }

    private void UpdateDayState() {
        currentDay = (Day)(((int)currentDay + 1) % weekLength);
        GWorld.Instance.SetDay(currentDay);
    }
    private void UpdateDays() {
        if (TimeOfDay >= 24f) {
            UpdateDayState(); 
            UpdateDaysUI();
        }
    }

    public float GetTime() {
        return days * TimeOfDay;
    }

    private void Update() {
        if (Preset == null)
            return;
        if (Application.isPlaying) {
            TimeOfDay += Time.deltaTime * timeFactor;
            UpdateDays();
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
        } else {
            UpdateLighting(TimeOfDay / 24f);
        }

        if (OnSevenOClock != null && TimeOfDay > 7 - timeThreshold && TimeOfDay < 7 + timeThreshold) {
            OnSevenOClock();
        }
        timeUI.text = "Hour: " + TimeSpan.FromHours(TimeOfDay).ToString(@"hh\:mm\:ss");
    }

    private void UpdateLighting(float timePercent) {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (directionalLight != null) {
            directionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360) - 90f, 180f, 0f));
        }
    }

    private void OnValidate()
    {
        if (directionalLight != null)
            return;
        if (RenderSettings.sun != null)
            directionalLight = RenderSettings.sun;
        else {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights) {
                if (light.type == LightType.Directional) {
                    directionalLight = light;
                    break;
                }
            }
        }
    }
}
