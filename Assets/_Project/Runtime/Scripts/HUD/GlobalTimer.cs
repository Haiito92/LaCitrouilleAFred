using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GlobalTimer : MonoBehaviour
{
   [SerializeField]private  TMP_Text _timer;
    private GameManager _gm;
    private void Start()
    {
        _gm = GameManager.Instance;
    }

    private void Update()
    {
        _timer.text = $"{(int)(_gm.GameTime- _gm.TimerGame)} ";
    }
}
