using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        requestMusic();
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
            musicplayer.playMusic(MusicPlayer.MusicType.MENU);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
