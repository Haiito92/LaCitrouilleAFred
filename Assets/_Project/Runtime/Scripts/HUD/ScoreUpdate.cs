using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField]TMP_Text _scoreDisplay;
    private UnityEvent OnChagedScore;

    private void Start()
    {
        UpdateScore();
    }
    // Update is called once per frame
    void UpdateScore()
    {
        _scoreDisplay.text = $"Score:{GameManager.Instance.Score}";
    }
}
