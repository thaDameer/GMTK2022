using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject ClearScreen;
    [SerializeField] private TextMeshProUGUI timerLabel;
    
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UIManager is NULL");
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else instance = this;
        
        DontDestroyOnLoad(this);
    }
    
    public void ShowGameOverScreen()
    {
        GameOverScreen.SetActive(true);
    }

    public void HideGameOverScreen()
    {
        GameOverScreen.SetActive(false);
    }

    public void ShowLevelClearScreen()
    {
        ClearScreen.SetActive(true);
    }

    public void HideLevelClearScreen()
    {
        ClearScreen.SetActive(false);
    }

    public void UpdateTimer(float time)
    {
        timerLabel.text = time.ToString("00.0");
    }
}