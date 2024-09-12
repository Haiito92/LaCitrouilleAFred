using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordList : MonoBehaviour
{
    [SerializeField] List<Theme> _themes;
    [SerializeField] TMP_Text _upText;
    [SerializeField] TMP_Text _downText;
    [SerializeField] TMP_Text _leftText;
    [SerializeField] TMP_Text _rightText;
    private int _chosenTheme;

    internal List<Theme> Themes { get => _themes; }

    private void Start()
    {

    }
    public void ShuffleWord()
    {
        if (_themes.Count > 0)
        {
            _chosenTheme = GameManager.Instance.WhichTheme;
            int randU = UnityEngine.Random.Range(0, _themes[_chosenTheme].WordListUp.Count);
            int randD = UnityEngine.Random.Range(0, _themes[_chosenTheme].WordListDown.Count);
            int randL = UnityEngine.Random.Range(0, _themes[_chosenTheme].WordListLeft.Count);
            int randR = UnityEngine.Random.Range(0, _themes[_chosenTheme].WordListRight.Count);
            _upText.text = _themes[_chosenTheme].WordListUp[randU];
            _downText.text = _themes[_chosenTheme].WordListDown[randD];
            _leftText.text = _themes[_chosenTheme].WordListLeft[randL];
            _rightText.text = _themes[_chosenTheme].WordListRight[randR];
            _themes.RemoveAt(_chosenTheme);
        }
    }
}
[Serializable]
class Theme
{
    [SerializeField] string _themeName;
    [SerializeField] List<string> _wordListUp = new List<string>();
    [SerializeField] List<string> _wordListLeft = new List<string>();
    [SerializeField] List<string> _wordListDown = new List<string>();
    [SerializeField] List<string> _wordListRight = new List<string>();

    public List<string> WordListUp { get => _wordListUp; }
    public List<string> WordListLeft { get => _wordListLeft; }
    public List<string> WordListDown { get => _wordListDown; }
    public List<string> WordListRight { get => _wordListRight; }
}
