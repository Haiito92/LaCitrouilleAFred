using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject _parent;

    [SerializeField] private Image _background;
    [SerializeField] private Color _victoryColor;
    [SerializeField] private Color _defeatColor;
    
    public void ShowVictoryScreen()
    {
        _background.color = _victoryColor;
        
        ShowEndScreen();
    }

    public void ShowDefeatScreen()
    {
        _background.color = _defeatColor;
        
        ShowEndScreen();
    }

    private void ShowEndScreen()
    {
        _parent.SetActive(true);
    }

    private void HideEndScreen()
    {
        _parent.SetActive(false);
    }
}
