using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager Instance;

    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioSource musicSource;

    private void Awake()
    {
        Instance = this;
    }


    public void LoadAudio (string audioName, Source source)
    {
        switch (source)
        {
            case (Source.Sound): { soundSource.clip = Resources.Load("Sounds/" + audioName) as AudioClip; break; }
            case (Source.Music): { musicSource.clip = Resources.Load("Sounds/" + audioName) as AudioClip; break; }
        }
    }


    public void PlayAudio (Source source)
    {
        switch (source)
        {
            case (Source.Sound): { soundSource.Play(); break; }
            case (Source.Music): { musicSource.Play(); break; }
        }
    }


    public void PauseAudio(Source source)
    {
        switch (source)
        {
            case (Source.Sound): { soundSource.Pause(); break; }
            case (Source.Music): { musicSource.Pause(); break; }
        }
    }


    public enum Source
    {
        Sound,
        Music
    }
}
