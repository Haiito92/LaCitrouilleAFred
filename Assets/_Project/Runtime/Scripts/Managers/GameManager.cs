using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //FirstLevelToLoad
    [SerializeField] private string _firstLevelName;
    
    //GameTimer
    [SerializeField] private float _gameTime;
    private float _timerGame;
    private bool IsGameStarted;
    
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    private void Update()
    {
        if (IsGameStarted)
        {
            _timerGame += Time.deltaTime;
        }

        if (IsGameStarted && _timerGame >= _gameTime)
        {
            EndGame(false);
        }
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(_firstLevelName);
        
        StartGame();
    }

    public void StartGame()
    {
        _timerGame = 0f;
        IsGameStarted = true;
    }

    public void EndGame(bool IsVictorious)
    {
        IsGameStarted = false;
    }
}
