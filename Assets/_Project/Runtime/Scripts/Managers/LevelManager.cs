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
    
    //LevelTimer
    [SerializeField] private float _levelTime;
    private float _timer;
    private bool _isLevelTimerPaused;

    //Actions
    public event UnityAction OnLevelEnded;
    
    //UnityEvents 
    [SerializeField] private UnityEvent OnLevelEndedEvent;
    [SerializeField] private WordList _wordList;

    public float LevelTime { get => _levelTime;}
    public float Timer { get => _timer;}

    private void Awake()
    {
        OnLevelEndedEvent.AddListener(()=>OnLevelEnded?.Invoke());
    }

    private void Start()
    {
        _timer = 0;
        if (_nextLevelName == "Final")
        {
            GameManager.Instance.ShuffleWords();
        }
        _wordList.ShuffleWord();
    }

    private void Update()
    {
        if (_timer < _levelTime && !_isLevelTimerPaused)
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
        PauseLevelTimer();
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
        GameManager.Instance.EndGame(IsVictorious);
    }

    private void PauseLevelTimer() => _isLevelTimerPaused = true;
    
    public void PauseGlobalTimer() => GameManager.Instance.PauseGlobalTimer();
    public void ResumeGlobalTimer() => GameManager.Instance.ResumeGlobalTimer();

    private void OnEnable()
    {
        GameManager.Instance.OnGameEnded += PauseLevelTimer;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameEnded -= PauseLevelTimer;
    }
}
