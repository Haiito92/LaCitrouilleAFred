using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] string _scene;
    public void LeaveGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(_scene);
    }
}
