using UnityEngine;

public class CubeCanvas : MonoBehaviour
{
    private void Awake()
    {
        EventBroker.Instance.OnLevelCountdownStart += Hide;
        EventBroker.Instance.OnStartLevel += Show;
    }

    private void OnDestroy()
    {
        EventBroker.Instance.OnLevelCountdownStart -= Hide;
        EventBroker.Instance.OnStartLevel -= Show;
    }
    private void Hide(float value) => Display(false);
    private void Show() => Display(true);
    private void Display(bool show)
    {
        gameObject.SetActive(show);
    }
}