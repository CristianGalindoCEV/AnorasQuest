﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;

    public AudioClip clip;
    public AudioMixer audioMixer;
    public float minPitch;
    public float maxPitch;

    [Range(0f, 1f)]
    public float volume;
    [Range (-3f, 3f)]
    public float pitch;



    public bool loop;

    [HideInInspector]
    public AudioSource source;


}
