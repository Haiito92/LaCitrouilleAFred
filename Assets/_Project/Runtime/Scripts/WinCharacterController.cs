using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCharacterController : MonoBehaviour
{
    private bool _triggeredRight;
    private bool _triggeredLeft;
    private bool _endOfTheBeginningOfTheEnd;
    private float _timer;
    Vector3 _basePos;
    Vector3 _baseSize;

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
            transform.localScale= Vector3.Lerp(_baseSize,_baseSize*1.5f, _timer/4);
            _timer += Time.deltaTime;
        }
        else if(!_endOfTheBeginningOfTheEnd)
        {
            _endOfTheBeginningOfTheEnd = true;
            _timer = 0;
        }
        if (_triggeredRight && _triggeredLeft&&_endOfTheBeginningOfTheEnd&& _timer < 2.5f)
        {             
            transform.position = Vector2.Lerp(_basePos, _limace.transform.position, _timer /2.5f);
            transform.Rotate(0, 0, 180  * Time.deltaTime);
            _timer += Time.deltaTime;
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
