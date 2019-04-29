using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSFXPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip smallHitSound;

    [SerializeField]
    AudioClip largeHitSound;

    [SerializeField]
    AudioClip groundHitSound;

    [SerializeField]
    AudioClip holeEnter;

    [SerializeField]
    AudioClip mulligan;

    [SerializeField]
    AudioClip fall;

    bool blockingPlay = false;
    AudioSource source;
    AudioSource environment;

    //GameObject musicplayerobj;
    // Start is called before the first frame update
    void Start()
    {

        requestMusic();

        AudioSource[] sources = GetComponents<AudioSource>();
        source = sources[0];
        environment = sources[1];
    }

    IEnumerator retry()
    {
        yield return new WaitForSeconds(1f);
        requestMusic();
    }

    void requestMusic()
    {
        MusicPlayer musicplayer = MusicPlayer.Instance;
        if (musicplayer == null)
        {
            StartCoroutine(retry());
        }
        else
        {
            musicplayer.playMusic(MusicPlayer.MusicType.LEVEL);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSmallHit()
    {
        source.clip = smallHitSound;
        source.Play();
    }

    public void playLargeHit()
    {
        source.clip = largeHitSound;
        source.Play();
    }

    public void playGroundHit()
    {
        source.clip = groundHitSound;
        source.Play();

    }

    public void playHoleEnter()
    {
        environment.clip = holeEnter;
        environment.Play();
    }

    public void playMulligan()
    {
            environment.clip = mulligan;
            environment.Play();
    }

    //IEnumerator delay()
    //{
    //    yield return new WaitForSeconds(1f);
    //   blockingPlay = false;
    //}

    public void playFall()
    {
        environment.clip = fall;
        environment.Play();
    }
}
