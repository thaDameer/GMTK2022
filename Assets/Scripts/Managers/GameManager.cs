using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region LevelVariables
    private int level;
    public GameObject startPos { get; set; }
    public GameObject endPos { get; set; }
    public float elapsedTime;
    public float handProgression;

    public bool playerDead;
    public bool levelCleared;
    public int score;

    #endregion

    public CubeController Player;
    public HandOfDeath Hand; 


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

        

        //GameOverScreen.SetActive(false); 
        //if(Player == null) Player = GameObject.FindWithTag("Player").GetComponent<CubeController>(); 
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        elapsedTime = 0;
        score = 0;
        playerDead = false;
        levelCleared = false;
        startPos = GameObject.FindWithTag("Start");
        endPos = GameObject.FindWithTag("End");
        if (Player == null) Player = GameObject.FindWithTag("Player").GetComponent<CubeController>();
        if (Hand == null) Hand = GameObject.FindWithTag("Hand").GetComponent<HandOfDeath>(); 
        if(Player && Hand)
            StartTimer();
        UIManager.Instance.HideLevelClearScreen(); 
        UIManager.Instance.HideGameOverScreen();
    }

    private void StartTimer()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (!playerDead)
        {
            elapsedTime += Time.deltaTime;
            UIManager.Instance.UpdateTimer();
            yield return null;
        }
    }
    private void Initialize()
    {

    }

    public void OnPlayerDead()
    {
        //Called from HandOfDeath
        //StopPlayerMovement(); 
        Player.isActive = false;
        UIManager.Instance.ShowGameOverScreen();
    }

    public void OnLevelClear()
    {
        score += (int)(100 * (1 - handProgression));
        Player.isActive = false;
        Hand.velocity = 0; 
        UIManager.Instance.ShowLevelClearScreen(); 
    }
}
