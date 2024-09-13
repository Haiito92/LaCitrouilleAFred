using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasNoDestroy : MonoBehaviour
{
    private CanvasNoDestroy _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
