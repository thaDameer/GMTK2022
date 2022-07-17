using TMPro;
using UnityEngine;
using UnityEngine.UI;
 

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject ClearScreen;
    [SerializeField] private TextMeshProUGUI timerLabel;
    [SerializeField] private Button restart, next; 

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

    private void Update()
    {
        UseKeysAsButtons(); 
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

    public void UseKeysAsButtons()
    {
        if (ClearScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                next.onClick.Invoke();
            }
        }

        if (GameOverScreen.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                restart.onClick.Invoke();
            }
        }
    }
}