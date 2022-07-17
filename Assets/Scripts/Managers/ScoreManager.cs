﻿using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    private float totalTime, levelTime;
    private bool timerPaused;
    
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get => instance;
        set
        {
            if (instance == null)
                instance = value;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;

        EventBroker.Instance.OnStartLevel += StartTimer;
        EventBroker.Instance.OnFailLevel += ResetLevelTimer;
        EventBroker.Instance.OnCompleteLevel += StopTimer;
        EventBroker.Instance.OnGameReset += ResetTotalTimer;
    }

    private void Update()
    {
        if (timerPaused) return;

        levelTime += Time.deltaTime;
    }

    private void StartTimer() => timerPaused = false;

    private void StopTimer()
    {
        timerPaused = true;
        totalTime += levelTime;
    }

    private void ResetLevelTimer() => levelTime = 0f;

    private void ResetTotalTimer() => totalTime = 0f;

    private void OnDestroy()
    {
        EventBroker.Instance.OnStartLevel -= StartTimer;
        EventBroker.Instance.OnFailLevel -= ResetLevelTimer;
        EventBroker.Instance.OnCompleteLevel -= StopTimer;
        EventBroker.Instance.OnGameReset -= ResetTotalTimer;
    } 
}