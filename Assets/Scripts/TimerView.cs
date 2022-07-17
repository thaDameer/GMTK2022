using System;
using System.Diagnostics.Eventing.Reader;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private void Awake()
    {
        gameObject.SetActive(false);
        
        EventBroker.Instance.OnStartLevel += Show;
        EventBroker.Instance.OnLevelCountdownStart += ResetTimerVisual;
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
    private void OnDestroy()
    {
        
        EventBroker.Instance.OnStartLevel -= Show;
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