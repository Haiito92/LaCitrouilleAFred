using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //FirstLevelToLoad
    [SerializeField] private string _firstLevelName;
    
    //GameTimer
    [SerializeField] private float _gameTime;
    private float _timerGame;
    private bool _isGameStarted;
    private bool _isTimerPaused;
    private WordList _wordList;
    private int _whichTheme;

    //EndGameMenu
    [SerializeField] private EndMenu _endMenu;
    
    //Score
    private int _score=600;
    [SerializeField] private LevelScoresSO _levelScoresSo;
    private List<LevelScoreWrapper> _copyScoresList;
    
    //UnityActions
    public event UnityAction OnGameEnded;

    public event UnityAction<int> OnScoreChanged;
    public event UnityAction OnScoreAdded;
    public event UnityAction OnScoreRemoved;
    
    //UnityEvents
    [SerializeField] private UnityEvent OnGameEndedEvent;

    [SerializeField] private UnityEvent OnScoreChangedEvent;
    [SerializeField] private UnityEvent OnScoreAddedEvent;
    [SerializeField] private UnityEvent OnScoreRemovedEvent;
    public float GameTime { get => _gameTime; }
    public float TimerGame { get => _timerGame; set => _timerGame = value; }
    
    public int WhichTheme { get => _whichTheme; set => _whichTheme = value; }

    public int Score
    {
        get => _score;
        private set
        {
            _score = Mathf.Clamp(value, 0, int.MaxValue);
            OnScoreChangedEvent.Invoke();
        }
    }

    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance => _instance;






    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        
        OnGameEndedEvent.AddListener(() => OnGameEnded?.Invoke());
        
        OnScoreChangedEvent.AddListener(()=>OnScoreChanged?.Invoke(Score));
        OnScoreAddedEvent.AddListener(() => OnScoreAdded?.Invoke());
        OnScoreRemovedEvent.AddListener(() => OnScoreRemoved?.Invoke());
    }

    private void Start()
    {
        _copyScoresList = new(_levelScoresSo.LevelScores);
    }

    private void Update()
    {
        if (_isGameStarted && !_isTimerPaused)
        {
            _timerGame += Time.deltaTime;
        }

        if (_isGameStarted && _timerGame >= _gameTime)
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
        ResetScore();
        _wordList = FindObjectOfType<WordList>();
       //_whichTheme=UnityEngine.Random.Range(0,_wordList.Themes.Count);
        _timerGame = 0f;
        _isGameStarted = true;
    }
    public void ShuffleWords()
    {
        _wordList = FindObjectOfType<WordList>();
        _wordList.Themes.RemoveAt(_whichTheme);
        _whichTheme = UnityEngine.Random.Range(0, _wordList.Themes.Count);
    }
    public void EndGame(bool IsVictorious)
    {
        _isGameStarted = false;
        PauseGlobalTimer();
        OnGameEndedEvent?.Invoke();
        _endMenu.ShowEndScreen(IsVictorious);
    }
    
    #region Timer
    public void PauseGlobalTimer()
    { 
        Debug.Log("PAUSE GLOBAL TIMER");
        _isTimerPaused = true;
    }
    public void ResumeGlobalTimer()
    {
        //Debug.Log("RESUME GLOBAL TIMER");
        _isTimerPaused = false;
    }
    #endregion

    #region Score

    public void AddScore(int valueAdded)
    {
        Score += valueAdded;
        OnScoreAddedEvent.Invoke();
    }

    public void RemoveScore(int valueRemoved)
    {
        Score -= valueRemoved;
        OnScoreRemovedEvent.Invoke();
    }
    
    private void ResetScore()
    {
        _levelScoresSo.LevelScores = new List<LevelScoreWrapper>(_copyScoresList);
    }
    #endregion
}
