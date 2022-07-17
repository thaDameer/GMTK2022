using System;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private void Awake()
    {
        EventBroker.Instance.OnLevelCountdownStart += ResetTimerVisual;
    }

    private void OnDestroy()
    {
        EventBroker.Instance.OnLevelCountdownStart -= ResetTimerVisual;
    }

    private void ResetTimerVisual(float timer)
    {
        timerText.text = TimeFormatHelperClass.FormatTime(0f);
    }
    private void Update()
    {
        if (ScoreManager.Instance && !ScoreManager.Instance.TimerPaused)
            timerText.text = TimeFormatHelperClass.FormatTime(ScoreManager.Instance.LevelTime);
    }
}