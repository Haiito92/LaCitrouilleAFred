using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StairTile : Tile
{
    [SerializeField] private string _nextLevelName;
    
    //Action
    public event UnityAction OnNextLevel;    
    
    //Unity Event
    [SerializeField] private UnityEvent OnNextLevelEvent;

    private void Awake()
    {
        OnNextLevelEvent.AddListener(() => OnNextLevel?.Invoke());
    }

    void ToNextLevel(string levelName)
    {
        OnNextLevelEvent?.Invoke();
        SceneManager.LoadScene(levelName);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Maybe add here a condition to check that its the player that is coming in the collider and not something else
        
        OnTriggerEnteredEvent?.Invoke();
        ToNextLevel(_nextLevelName);
    }
}
