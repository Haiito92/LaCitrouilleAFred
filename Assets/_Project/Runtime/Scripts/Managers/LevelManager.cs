using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool _isLevelTimerPaused = true;

    [SerializeField] private LevelScoresSO _levelScoresSO;
    
    //Actions
    public event UnityAction OnLevelResumed;
    public event UnityAction OnLevelPaused;
    public event UnityAction OnLevelEnded;
    
    //UnityEvents 

    [SerializeField] private UnityEvent OnLevelResumedEvent;
    [SerializeField] private UnityEvent OnLevelPausedEvent;
    [SerializeField] private UnityEvent OnLevelEndedEvent;
    [SerializeField] private WordList _wordList;

    public float LevelTime { get => _levelTime;}
    public float Timer { get => _timer;}

    private void Awake()
    {
        OnLevelResumedEvent.AddListener(()=>OnLevelResumed?.Invoke());
        OnLevelPausedEvent.AddListener(()=>OnLevelPaused?.Invoke());
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
        
        if(_timer >= _levelTime && !_isLevelTimerPaused)
        {
            EndLevel();
        }
    }

    public void EndLevel()
    {
        PauseLevelTimer();
        OnLevelEndedEvent?.Invoke();
    }

    #region LevelChange
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
        PauseLevelTimer();
        GameManager.Instance.EndGame(IsVictorious);
    }
    #endregion

    #region Timers
    public void PauseLevelTimer()
    {
        Debug.Log("PAUSE LEVEL TIMER");
        _isLevelTimerPaused = true;
        OnLevelPausedEvent.Invoke();
    }
    public void ResumeLevelTimer()
    {
        Debug.Log("RESUME LEVEL TIMER");
        _isLevelTimerPaused = false;
        OnLevelResumedEvent.Invoke();
    }
    
    public void PauseGlobalTimer() => GameManager.Instance.PauseGlobalTimer();
    public void ResumeGlobalTimer() => GameManager.Instance.ResumeGlobalTimer();
    #endregion

    public void GiveScore()
    {
        string levelName = SceneManager.GetActiveScene().name;
        //int scoreToGive = _levelScoresSO.LevelScores[levelName];
        int id =_levelScoresSO.LevelScores.GetIndex(levelName, out var  lsw);
        
        _levelScoresSO.LevelScores[id] = LevelScoreWrapper.DivideScore(lsw, 2);
        
        GameManager.Instance.AddScore(lsw.Score);
        //_levelScoresSO.DivideScore(lws);
        Debug.Log("Score Give : " + lsw.Score);
    }
    
    #region Enable&Disable
    private void OnEnable()
    {
        GameManager.Instance.OnGameEnded += PauseLevelTimer;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameEnded -= PauseLevelTimer;
    }
    #endregion
}
