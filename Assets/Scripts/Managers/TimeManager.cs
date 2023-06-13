using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    public static TimeManager timeManager;

    [SerializeField] private Transform directionalLight;
    [SerializeField] private float dayNightCircleDuration;

    [Header("UI")]
    // TODO: Better Access with UI Manager
    [SerializeField] private TMP_Text clockText;
    [SerializeField] private Image clockImage;
    
    public State TimeOfDay { get; private set; }

    private float timer;
    private Color dayColor = new(1f, 0.98f, 0f);
    private Color noonColor = new(1f, 0.54f, 0f);
    private Color nightColor = new(0.32f, 0.24f, 1f);

    private void Start() {
        timeManager = this;
        timer = 0;
        TimeOfDay = State.Day;
        clockText.text = TimeOfDay.ToString();
    }

    private void Update() {
        timer += Time.deltaTime;
    
        var t = Mathf.InverseLerp(0, dayNightCircleDuration, timer);
        if (t >= 1) { timer = 0; }
        
        switch (t) {
            case <= 0.33f:
                TimeOfDay = State.Day;
                clockImage.color = dayColor;
                break;
            case <= 0.66f:
                TimeOfDay = State.Noon;
                clockImage.color = noonColor;
                break;
            default:
                TimeOfDay = State.Night;
                clockImage.color = nightColor;
                break;
        }

        clockImage.fillAmount = t;
        clockText.text = TimeOfDay.ToString();
        directionalLight.rotation = Quaternion.Euler(Mathf.Lerp(0, 360, t), -30, 0);
    }

    public enum State {
        Day = 1,
        Noon = 2,
        Night = 3,
    }
}