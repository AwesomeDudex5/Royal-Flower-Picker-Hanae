using UnityEngine.Audio;
using System;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}


public class AudioM : MonoBehaviour
{
    public static AudioM instance;
    public AudioClip backgroundMusic;
    private AudioSource _audiosource;
    public Sound[] soundEffects;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        foreach (Sound s in soundEffects)
        {
            s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.current.gameOver += stopMusic;
        _audiosource = this.GetComponent<AudioSource>();
        playMusic();
    }

    void playMusic()
    {
        _audiosource.clip = backgroundMusic;
        _audiosource.loop = true;
        _audiosource.Play();
    }

    void stopMusic()
    {
        _audiosource.Pause();
    }

    public void playSound(string name)
    {
        Sound s = Array.Find(soundEffects, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }
}
