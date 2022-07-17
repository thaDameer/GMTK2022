using System.Collections;
using System.Collections.Generic;
using Tymski;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneController : ScriptableObject
{
    [SerializeField] private SceneReference[] scenes;

    private int activeIndex;

    public void LoadNext()
    {
        activeIndex++;
        if (IndexIsValid(activeIndex))
            SceneManager.LoadScene(scenes[activeIndex].ScenePath);
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
        SceneManager.LoadScene(scenes[index].ScenePath);
    }

    public void RestartActiveLevel() => SceneManager.LoadScene(scenes[activeIndex].ScenePath);

    public void LoadMainMenu()
    {
        activeIndex = 0;
        SceneManager.LoadScene(scenes[0].ScenePath);
    }

    public void Quit()
    {
        Application.Quit(); 
    }

    private bool IndexIsValid(int index) => index <= scenes.Length;
}
