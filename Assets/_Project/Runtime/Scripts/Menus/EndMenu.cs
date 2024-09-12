using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private UnityEvent _onDefeat;
    [SerializeField] private UnityEvent _onVictory;

    [SerializeField] private Image _background;
    [SerializeField] private Color _victoryColor;
    [SerializeField] private Color _defeatColor;

    private void ShowVictoryScreen()
    {
        _background.color = _victoryColor;
    }

    private void ShowDefeatScreen()
    {
        _background.color = _defeatColor;
    }

    public void ShowEndScreen(bool IsVictorious)
    {
        if (IsVictorious)
        {
            ShowVictoryScreen();
            _onVictory.Invoke();
        }
        else
        {
            ShowDefeatScreen();
            _onDefeat.Invoke();
        }
        _parent.SetActive(true);
    }

    public void HideEndScreen()
    {
        _parent.SetActive(false);
    }
}
