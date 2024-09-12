using System;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject _parent;

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
        }
        else
        {
            ShowDefeatScreen();
        }
        _parent.SetActive(true);
    }

    public void HideEndScreen()
    {
        _parent.SetActive(false);
    }
}
