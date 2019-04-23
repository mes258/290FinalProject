using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject musicplayerobj = GameObject.Find("MusicPlayer");
        if (musicplayerobj != null)
        {
            MusicPlayer musicplayer = musicplayerobj.GetComponent<MusicPlayer>();
            musicplayer.playMusic(MusicPlayer.MusicType.MENU);
            Debug.Log("playing music");
        }
        else
        {
            Debug.Log("No music found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
