﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    public enum MusicType
    {
        MENU, LEVEL
    }

    private MusicType type;
    private bool isawake = false;

    [SerializeField]
    AudioClip menuMusic;

    [SerializeField]
    AudioClip levelMusic;

    [SerializeField]
    AudioSource audioplayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void playMusic(MusicType typeIn)
    {

        if(type != typeIn || !isawake)
        {
            type = typeIn;
            if (typeIn == MusicType.MENU)
            {
                playTrack(menuMusic);
            }
            else if(typeIn == MusicType.LEVEL || !isawake)
            {
                playTrack(levelMusic);
            }
        }
        isawake = true;
    }

    private void playTrack(AudioClip clip)
    {
        audioplayer.Stop();
        audioplayer.clip = clip;
        audioplayer.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioplayer.loop = true;
    }
}
