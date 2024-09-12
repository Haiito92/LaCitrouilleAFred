using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelTimer : MonoBehaviour
{
   [SerializeField] private Slider _lvlTimer;
   [SerializeField] private LevelManager _lvlManager;
    // Start is called before the first frame update
    void Start()
    {
        _lvlTimer.maxValue = _lvlManager.LevelTime;
        _lvlTimer.value = _lvlTimer.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        _lvlTimer.value = _lvlTimer.maxValue - _lvlManager.Timer;
    }
}
