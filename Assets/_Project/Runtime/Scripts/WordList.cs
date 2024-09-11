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
    private void Start()
    {
        _chosenTheme = UnityEngine.Random.Range(0, _themes.Count);
        Debug.Log(_chosenTheme);
        ShuffleWord();
    }
    public void ShuffleWord()
    {
        int randU = UnityEngine.Random.Range(0, _themes[_chosenTheme].WordListUp.Count);
        int randD = UnityEngine.Random.Range(0, _themes[_chosenTheme].WordListDown.Count);
        int randL = UnityEngine.Random.Range(0, _themes[_chosenTheme].WordListLeft.Count);
        int randR = UnityEngine.Random.Range(0, _themes[_chosenTheme].WordListRight.Count);
        Debug.Log(randU);
        _upText.text = _themes[_chosenTheme].WordListUp[randU];
        _downText.text = _themes[_chosenTheme].WordListDown[randD];
        _leftText.text = _themes[_chosenTheme].WordListLeft[randL];
        _rightText.text = _themes[_chosenTheme].WordListRight[randR];
        _themes[_chosenTheme].WordListUp.RemoveAt(randU);
        _themes[_chosenTheme].WordListDown.RemoveAt(randD);
        _themes[_chosenTheme].WordListLeft.RemoveAt(randL);
        _themes[_chosenTheme].WordListRight.RemoveAt(randR);
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

    public List<string> WordListUp { get => _wordListUp;}
    public List<string> WordListLeft { get => _wordListLeft;}
    public List<string> WordListDown { get => _wordListDown;}
    public List<string> WordListRight { get => _wordListRight;}
}
