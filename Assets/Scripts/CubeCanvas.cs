using UnityEngine;

public class CubeCanvas : MonoBehaviour
{
    private void Awake()
    {
        EventBroker.Instance.OnLevelCountdownStart += Hide;
        EventBroker.Instance.OnStartLevel += Show;
        EventBroker.Instance.OnCompleteLevel += Hide;
    }

    private void OnDestroy()
    {
        EventBroker.Instance.OnLevelCountdownStart -= Hide;
        EventBroker.Instance.OnStartLevel -= Show;
        EventBroker.Instance.OnCompleteLevel -= Hide;
    }

    private void Hide() => Display(false);
    private void Hide(float value) => Display(false);
    private void Show() => Display(true);
    private void Display(bool show)
    {
        gameObject.SetActive(show);
    }
}