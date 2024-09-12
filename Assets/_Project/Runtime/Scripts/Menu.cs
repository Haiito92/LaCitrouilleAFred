using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LeaveGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        GameManager.Instance.LoadFirstLevel();
    }
}
