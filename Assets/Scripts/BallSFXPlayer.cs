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
    // Start is called before the first frame update
    void Start()
    {
        GameObject musicplayerobj = GameObject.Find("MusicPlayer");
        if (musicplayerobj != null)
        {
            MusicPlayer musicplayer = musicplayerobj.GetComponent<MusicPlayer>();
            musicplayer.playMusic(MusicPlayer.MusicType.LEVEL);
            Debug.Log("playing music");
        }
        else
        {
            Debug.Log("No music found");
        }

        AudioSource[] sources = GetComponents<AudioSource>();
        source = sources[0];
        environment = sources[1];
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
        //if (!blockingPlay)
        //{
            source.clip = groundHitSound;
            source.Play();
        //}
    }

    public void playHoleEnter()
    {
        //blockingPlay = true;
        environment.clip = holeEnter;
        environment.Play();
        //StartCoroutine(delay());
    }

    public void playMulligan()
    {
        //if(!blockingPlay)
        //{
            environment.clip = mulligan;
            environment.Play();
        //}
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
