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
        int scoreToGive = _levelScoresSO.LevelScores[levelName];
        GameManager.Instance.AddScore(scoreToGive);
        Debug.Log("Score Give : " + scoreToGive);
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
