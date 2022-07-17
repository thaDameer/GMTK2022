﻿using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public bool TimerPaused { get; private set; }
    public float LevelTime { get; private set; }
    public float TotalTime { get; private set; }

    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get => instance;
        private set
        {
            if (instance == null)
                instance = value;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;

        EventBroker.Instance.OnStartLevel += StartTimer;
        EventBroker.Instance.OnFailLevel += ResetLevelTimer;
        EventBroker.Instance.OnCompleteLevel += StopTimer;
        EventBroker.Instance.OnGameReset += ResetTotalTimer;
    }

    private void Update()
    {
        if (TimerPaused) return;

        LevelTime += Time.deltaTime;
    }

    private void StartTimer()
    {
        ResetLevelTimer();
        TimerPaused = false;
    }

    private void StopTimer()
    {
        TimerPaused = true;
        TotalTime += LevelTime;
    }

    private void ResetLevelTimer() => LevelTime = 0f;

    private void ResetTotalTimer() => TotalTime = 0f;

    private void OnDestroy()
    {
        EventBroker.Instance.OnStartLevel -= StartTimer;
        EventBroker.Instance.OnFailLevel -= ResetLevelTimer;
        EventBroker.Instance.OnCompleteLevel -= StopTimer;
        EventBroker.Instance.OnGameReset -= ResetTotalTimer;
    } 
}