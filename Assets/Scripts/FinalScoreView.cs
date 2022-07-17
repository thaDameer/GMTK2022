using System;
using TMPro;
using UnityEngine;

public class FinalScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;

    private void Start()
    {
        scoreLabel.text = ScoreManager.Instance.TotalTime.ToString("00.0");
    }
}