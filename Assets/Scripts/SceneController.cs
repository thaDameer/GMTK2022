using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneController : ScriptableObject
{
    [SerializeField] private SceneAsset[] scenes;

    private int activeIndex;

    public void LoadNext()
    {
        activeIndex++;
        if (IndexIsValid(activeIndex))
            SceneManager.LoadScene(scenes[activeIndex].name);
        else
            LoadMainMenu();
    }

    public void LoadLevel(int index)
    {
        if (!IndexIsValid(index))
        {
            LoadMainMenu();
            return;
        }
        
        activeIndex = index;
        SceneManager.LoadScene(scenes[index].name);
    }

    public void RestartActiveLevel() => SceneManager.LoadScene(scenes[activeIndex].name);

    public void LoadMainMenu()
    {
        activeIndex = 0;
        SceneManager.LoadScene(scenes[0].name);
    }

    private bool IndexIsValid(int index) => index <= scenes.Length;
}
