using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUIView : MonoBehaviour
{
    private void Awake()
    {
        EventBroker.Instance.OnGameplaySceneLoaded += HideUI;
        EventBroker.Instance.OnStartLevel += ShowUI;
        EventBroker.Instance.OnLevelCountdownStart += HideUI;
    }

    private void OnDestroy()
    {
        EventBroker.Instance.OnGameplaySceneLoaded -= HideUI;
        EventBroker.Instance.OnStartLevel -= ShowUI;
        EventBroker.Instance.OnLevelCountdownStart -= HideUI;
    }

    private void HideUI() => DisplayUI(false);
    private void HideUI(float value) => DisplayUI(false);
    private void ShowUI() => DisplayUI(true);
    private void DisplayUI(bool show)
    {
        gameObject.SetActive(show);
    }
}