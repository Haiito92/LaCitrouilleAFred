using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private UnityEvent _onDefeat;
    [SerializeField] private UnityEvent _onVictory;

    //Background
    [SerializeField] private Image _background;
    [SerializeField] private Image _background2;
    [SerializeField] private Sprite _winScreen;
    [SerializeField] private Sprite _loseScreen;

    //For Test
    [SerializeField] private Color _victoryColor;
    [SerializeField] private Color _defeatColor;

    private void ShowVictoryScreen()
    {
        //_background.color = _victoryColor;
        _background.sprite = _winScreen;
        _background2.sprite = _winScreen;
    }

    private void ShowDefeatScreen()
    {
        //_background.color = _defeatColor;
        _background.sprite = _loseScreen;
        _background2.sprite = _loseScreen;
    }

    public void ShowEndScreen(bool IsVictorious)
    {
        if (IsVictorious)
        {
            ShowVictoryScreen();
            FindObjectOfType<AudioManager>().Play("sfx_win");
            _onVictory.Invoke();

        }
        else
        {
            ShowDefeatScreen();
            FindObjectOfType<AudioManager>().Play("sfx_lose");
            _onDefeat.Invoke();
        }
        _parent.SetActive(true);
    }

    public void HideEndScreen()
    {
        _parent.SetActive(false);
    }
}
