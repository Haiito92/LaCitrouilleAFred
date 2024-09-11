using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    //Levels
    [SerializeField] private string _previousLevelName;
    [SerializeField] private string _nextLevelName;

    //Menu
    [SerializeField] private EndMenu _endMenu;

    //LevelTimer
    [SerializeField] private float _levelTime;
    private float _timer;

    //Actions
    public event UnityAction OnLevelEnded;
    
    //UnityEvents 
    [SerializeField] private UnityEvent OnLevelEndedEvent;

    private void Awake()
    {
        OnLevelEndedEvent.AddListener(()=>OnLevelEnded?.Invoke());
    }

    private void Start()
    {
        _timer = 0;
    }

    private void Update()
    {
        if (_timer < _levelTime)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            EndLevel();
        }
    }

    private void EndLevel()
    {
        OnLevelEndedEvent?.Invoke();
    }
    
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

    public void EndGame(bool IsVictorious)
    {
        if (IsVictorious)
        {
            _endMenu.ShowVictoryScreen();
        }
        else
        {
            _endMenu.ShowDefeatScreen();
        }
    }
}
