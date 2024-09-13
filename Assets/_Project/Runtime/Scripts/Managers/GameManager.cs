using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //LevelsToLoad
    [SerializeField] private string _firstLevelName;
    [SerializeField] private string _endLevelName;
    [SerializeField] private string _mainMenuName;
    [SerializeField] private string _finalSequenceLevelName;
    
    //GameTimer
    [SerializeField] private float _gameTime;
    private float _timerGame;
    private bool _isGameStarted;
    private bool _isTimerPaused;
    private WordList _wordList;
    private int _whichTheme;

    //End
    public bool IsVictory { get; set; }
    
    //Score
    private int _score;
    [SerializeField] private LevelScoresSO _levelScoresSo;
    private List<LevelScoreWrapper> _copyScoresList;
    
    //UnityActions
    public event UnityAction OnGameEnded;
    public event UnityAction OnGlobalTimerEnded;

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
            EndTimer();
        }
    }


    #region LoadScenes
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(_firstLevelName);
        
        StartGame();
    }

    public void LoadEndGameMenu()
    {
        SceneManager.LoadScene(_endLevelName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(_mainMenuName);
    }

    public void LoadFinalSequence()
    {
        SceneManager.LoadScene(_finalSequenceLevelName);
    }
    #endregion
    
    
    public void StartGame()
    {
        ResetScore();
        _wordList = FindObjectOfType<WordList>();
       //_whichTheme=UnityEngine.Random.Range(0,_wordList.Themes.Count);
        _timerGame = 0f;
        _isGameStarted = true;
    }
    
    
    
    private void EndTimer()
    {
        OnGlobalTimerEnded?.Invoke();
    }
    
    public void EndGame()
    {
        _isGameStarted = false;
        PauseGlobalTimer();
        OnGameEndedEvent?.Invoke();
        
        SceneManager.LoadScene(_endLevelName);
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
