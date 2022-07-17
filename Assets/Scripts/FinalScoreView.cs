using System;
using TMPro;
using UnityEngine;

public class FinalScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;

    private void Start()
    {
        var timeSpan = TimeSpan.FromSeconds(ScoreManager.Instance.TotalTime);
        scoreLabel.text = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00} min";
    }
}