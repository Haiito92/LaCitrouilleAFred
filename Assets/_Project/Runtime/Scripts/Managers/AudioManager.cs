using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public Audio[] _audio;

    private void Awake()
    {
        foreach(Audio audio in _audio)
        {
            audio._source = gameObject.AddComponent<AudioSource>();
            audio._source.clip = audio._clip;

            audio._source.volume = audio._volume;
            audio._source.pitch = audio._pitch;
        }
    }

    private void Start()
    {
        Play("MusicTheme");
    }

    public void Play(String name)
    {
        Audio a = Array.Find(_audio, audio => audio._name == name);
        if (a == null)
        {
            Debug.LogWarning("Sound:" + name + "not Found");
            return;
        }
        a._source.Play();
    }

}
