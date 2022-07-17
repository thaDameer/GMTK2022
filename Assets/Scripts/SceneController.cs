using System.Linq;
using Tymski;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneController : ScriptableObject
{
    [SerializeField] private SceneReference[] scenes;

    private int GetActiveIndex()
    {
        var currentScenePath = SceneManager.GetActiveScene().path;
        return scenes.TakeWhile(scene => scene.ScenePath != currentScenePath).Count();
    }
    
    public void LoadNext()
    {
        var nextIndex = GetActiveIndex() + 1;
        
        if (IndexIsValid(nextIndex))
            SceneManager.LoadScene(scenes[nextIndex].ScenePath);
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
        
        SceneManager.LoadScene(scenes[index].ScenePath);
    }

    public void RestartActiveLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void LoadFirstLevel()
    {
        EventBroker.Instance.OnGameReset?.Invoke();
        SceneManager.LoadScene(scenes[1].ScenePath);
    }
    
    public void LoadMainMenu() => SceneManager.LoadScene(scenes[0].ScenePath);

    public void Quit() => Application.Quit();

    private bool IndexIsValid(int index) => index < scenes.Length;
}
