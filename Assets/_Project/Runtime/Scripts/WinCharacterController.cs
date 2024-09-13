using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class WinCharacterController : MonoBehaviour
{
    private bool _triggeredRight;
    private bool _triggeredLeft;
    private bool _endOfTheBeginningOfTheEnd;
    private bool _end;
    private bool _start;
    private float _timer;
    private int _score;
    private int _scoreMax;
    private Vector3 _basePos;
    private Vector3 _baseSize;
    [SerializeField] private TMP_Text _scoreDisplay;
    [SerializeField] private GameObject _rt;
    [SerializeField] private GameObject _lt;
    [SerializeField] private GameObject _limace;

    [SerializeField] private UnityEvent _onEndSequence;

    [SerializeField] private string _endSceneName;
    private void Start()
    {
        
    }
    public void StartSequence()
    {
        _score = GameManager.Instance.Score;
        Debug.Log("2");
        _scoreMax = _score;
        _baseSize = transform.localScale;
        _basePos = transform.position;
         _timer = 0;
         _start = true;
    }
    private void Update()
    {
        if (_timer < 4&&!_endOfTheBeginningOfTheEnd&&_start)
        {
            Debug.Log(name);
            _score = (int)Mathf.Lerp(_scoreMax, 0, _timer / 4);
            transform.localScale= Vector3.Lerp(_baseSize,_baseSize+_baseSize*(_scoreMax/800), _timer/4);
            _scoreDisplay.text = $"{_score}";
            _timer += Time.deltaTime;
        }
        else if(!_endOfTheBeginningOfTheEnd&&_start)
        {
            _endOfTheBeginningOfTheEnd = true;
            _timer = 0;
            _scoreDisplay.gameObject.SetActive(false);
            _rt.SetActive(true);
            _lt.SetActive(true);
        }
        if (_triggeredRight && _triggeredLeft&&_endOfTheBeginningOfTheEnd&& _timer < 2.5f)
        {
            _rt.SetActive(false);
            _lt.SetActive(false);
            transform.position = Vector2.Lerp(_basePos, _limace.transform.position, _timer /2.5f);
            transform.Rotate(0, 0, 180  * Time.deltaTime);
            _timer += Time.deltaTime;
        }
        else if (_triggeredRight && _triggeredLeft && _endOfTheBeginningOfTheEnd)
        {
            _end = true;
        }
        if (_end)
        {
            _onEndSequence.Invoke();
            if (_limace.transform.localScale.magnitude > transform.localScale.magnitude)
            {
                GameManager.Instance.IsVictory = false;
            }
            else
            {
                GameManager.Instance.IsVictory = true;
            }
        }
    }
    public void Trigger1()
    {
        Debug.Log("ICI");
        _triggeredLeft = true;
    }
    public  void Trigger2()
    {
        Debug.Log("LA");
        _triggeredRight = true;
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene(_endSceneName);
    }
}
