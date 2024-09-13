using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScore : MonoBehaviour
{
    [SerializeField]TMP_Text _scoreDisplay1;
    [SerializeField]TMP_Text _scoreDisplay2;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddScore(10 * (int)(GameManager.Instance.GameTime - GameManager.Instance.TimerGame));
        _scoreDisplay1.text = $"{GameManager.Instance.Score}";
        _scoreDisplay2.text = $"{GameManager.Instance.Score}";
    }

}
