using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StairTile : Tile
{
    [SerializeField] private string _nextLevelName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Maybe add here a condition to check that its the player that is coming in the collider and not something else
        OnTriggerEnteredEvent?.Invoke();
    }
}
