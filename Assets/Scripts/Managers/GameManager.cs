using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region LevelVariables
    private int level;
    public GameObject StartPos { get; private set; }
    public GameObject EndPos { get; private set; }
    public float HandProgression { get; set; }
    public bool PlayerDead { get; private set; }

    #endregion

    [SerializeField] private float countdownTime = 2f;
    
    private CubeController player;
    private HandOfDeath hand;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameManager is NULL");
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnSceneLoaded;

        EventBroker.Instance.OnCompleteLevel += OnLevelComplete;
        EventBroker.Instance.OnFailLevel += OnPlayerDead;

        //GameOverScreen.SetActive(false); 
        //if(Player == null) Player = GameObject.FindWithTag("Player").GetComponent<CubeController>(); 
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        EventBroker.Instance.OnCompleteLevel -= OnLevelComplete;
        EventBroker.Instance.OnFailLevel -= OnPlayerDead;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerDead = false;
        StartPos = GameObject.FindWithTag("Start");
        EndPos = GameObject.FindWithTag("End");
        
        if (player == null) player = GameObject.FindWithTag("Player")?.GetComponent<CubeController>();
        if (hand == null) hand = GameObject.FindWithTag("Hand")?.GetComponent<HandOfDeath>();
        if (player && hand)
        {
            StartCoroutine(StartLevelCountdown(countdownTime));
        }
        
        UIManager.Instance.HideLevelClearScreen(); 
        UIManager.Instance.HideGameOverScreen();
    }

    private IEnumerator StartLevelCountdown(float time)
    {
        EventBroker.Instance.OnLevelCountdownStart?.Invoke(time);
        yield return new WaitForSeconds(time);
        EventBroker.Instance.OnStartLevel?.Invoke();
    }

    public void OnPlayerDead()
    {
        PlayerDead = true;
        player.isActive = false;
        UIManager.Instance.ShowGameOverScreen();
    }

    public void OnLevelComplete()
    {
        player.isActive = false;
        hand.velocity = 0; 
        UIManager.Instance.ShowLevelClearScreen(); 
    }
}