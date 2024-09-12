using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCharacterController : MonoBehaviour
{
    private bool _triggeredRight;
    private bool _triggeredLeft;
    private bool _endOfTheBeginningOfTheEnd;
    private bool _end;
    private float _timer;
    private Vector3 _basePos;
    private Vector3 _baseSize;
    [SerializeField] private GameObject _rt;
    [SerializeField] private GameObject _lt;
    [SerializeField] private GameObject _limace;
    private void Start()
    {
        _baseSize = transform.localScale;
        _basePos = transform.position;
         _timer = 0;
    }
    private void Update()
    {
        if (_timer < 4&&!_endOfTheBeginningOfTheEnd)
        {
            transform.localScale= Vector3.Lerp(_baseSize,_baseSize*5f, _timer/4);
            _timer += Time.deltaTime;
        }
        else if(!_endOfTheBeginningOfTheEnd)
        {
            _endOfTheBeginningOfTheEnd = true;
            _timer = 0;
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
            _end = true;
        }
        if (_end)
        {
            if (_limace.transform.localScale.magnitude > transform.localScale.magnitude)
            {
                GameManager.Instance.EndGame(false);
            }
            else
            {
                GameManager.Instance.EndGame(true);
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
}
