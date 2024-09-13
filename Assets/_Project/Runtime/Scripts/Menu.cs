using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject _startTimeline;
    public void LeaveGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        GameManager.Instance.LoadFirstLevel();
    }

    public void DeactivateTimelineStart()
    {
        _startTimeline.SetActive(false);
    }
}
