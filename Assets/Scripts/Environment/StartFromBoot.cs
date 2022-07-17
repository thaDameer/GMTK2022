using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartFromBoot : MonoBehaviour
{
    [MenuItem("DiceHard/Play from boot")]
    static void PlayBoot()
    {
        Debug.Log("hooh");
        var boot = EditorSceneManager.GetSceneByName("MainMenu");
        
        Debug.Log(boot.path);
        EditorSceneManager.OpenScene(boot.path, OpenSceneMode.Single);
        EditorApplication.isPlaying = true;
        return;
        
    }

    
    private void Update()
    {
            
    }
}