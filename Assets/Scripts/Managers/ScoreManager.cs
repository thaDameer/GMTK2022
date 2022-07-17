using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    
    private static ScoreManager instance;

    public static ScoreManager Instance
    {
        get => instance;
        set
        {
            if (instance == null)
                instance = value;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        instance = this;
    }
    
    //score += (int)(100 * (1 - HandProgression));
}