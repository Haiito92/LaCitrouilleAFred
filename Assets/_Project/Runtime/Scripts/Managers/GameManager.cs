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
    
    //EndGameMenu
    [SerializeField] private EndMenu _endMenu;
    
    //UnityActions
    public event UnityAction OnGameEnded;
    
    //UnityEvents
    [SerializeField] private UnityEvent OnGameEndedEvent;

    public float TimerGame { get => _timerGame;}
    public float GameTime { get => _gameTime; }


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
        _timerGame = 0f;
        _isGameStarted = true;
    }

    public void EndGame(bool IsVictorious)
    {
        OnGameEndedEvent?.Invoke();
        _isGameStarted = false;
        PauseGlobalTimer();
        _endMenu.ShowEndScreen(IsVictorious);
    }

    public void PauseGlobalTimer()
    { 
        //Debug.Log("PAUSE GLOBAL TIMER");
        _isTimerPaused = true;
    }
    public void ResumeGlobalTimer()
    {
        //Debug.Log("RESUME GLOBAL TIMER");
        _isTimerPaused = false;
    }
}
