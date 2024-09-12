using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("failed audio manager");
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

}
