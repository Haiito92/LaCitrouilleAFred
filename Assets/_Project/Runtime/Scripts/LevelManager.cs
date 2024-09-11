using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void ChangeLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
