using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string _previousLevelName;
    [SerializeField] private string _nextLevelName;

    public void FallToPreviousLevel()
    {
        ChangeLevel(_previousLevelName);
    }

    public void ToNextLevel()
    {
        ChangeLevel(_nextLevelName);
    }
    
    private void ChangeLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
