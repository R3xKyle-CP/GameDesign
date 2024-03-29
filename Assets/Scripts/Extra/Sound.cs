﻿//Kyle Ringler wrote this script
using UnityEngine.Audio;
using UnityEngine;
using System;

[System.Serializable]
public class Sound {

    public string name;

    public AudioClip clip;

    [Range(0.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float volume;


    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
