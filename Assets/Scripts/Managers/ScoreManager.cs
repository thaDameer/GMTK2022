using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    private bool timerPaused;

    public float LevelTime { get; private set; }
    public float TotalTime { get; private set; }

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

        LevelTime += Time.deltaTime;
    }

    private void StartTimer()
    {
        ResetLevelTimer();
        timerPaused = false;
    }

    private void StopTimer()
    {
        timerPaused = true;
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