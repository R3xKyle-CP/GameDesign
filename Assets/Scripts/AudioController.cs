using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : Singleton<AudioController> {

    public Sound[] sounds;

    void Awake()
    {

        DontDestroyOnLoad(gameObject);

        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    void Start()
    {
        Play("BackgroundMusic");
    }

	public void Play(string name)
    {
        Sound playSound = Array.Find(sounds, sound => sound.name == name);
        if (playSound == null)
        {
            Debug.LogWarning("Sound not found");
            return;
        }
        playSound.source.Play();
    }
}
