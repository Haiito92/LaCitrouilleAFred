using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasNoDestroy : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
