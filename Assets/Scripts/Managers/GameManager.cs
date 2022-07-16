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
    public float levelTime;
    public float handProgression;

    public bool playerDead;
    public bool levelCleared; 
    public int score; 

    #endregion


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
        instance = this;
        DontDestroyOnLoad(this); 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        levelTime += Time.deltaTime; 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelTime = 0;
        score = 0; 
        playerDead = false;
        levelCleared = false; 
        startPos = GameObject.FindWithTag("Start");
        endPos = GameObject.FindWithTag("End");
    }

    public void OnPlayerDead()
    {
        //Called from HandOfDeath
        //StopPlayerMovement(); 
    }

    private void OnLevelClear()
    {
        score += (int) (100 * (1 - handProgression));
    }
}
