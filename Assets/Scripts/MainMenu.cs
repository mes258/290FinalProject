using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private VolumePanel volumePanel;

    [SerializeField]
    private GameObject creditsPanel;

    //[SerializeField]
    //private GameObject levelSelect;

    private GameObject musicplayerobj;
    private MusicPlayer musicplayer;
    // Start is called before the first frame update

    
    void Start()
    {
        musicplayerobj = GameObject.Find("MusicPlayer");
        if(musicplayerobj != null)
        {
            musicplayer = musicplayerobj.GetComponent<MusicPlayer>();
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

    public void startGame()
    {
        //SceneManager.LoadScene("Level1");
        playLevel("Level1");
        Debug.Log("start");
    }

    public void playLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void showVolume()
    {
        volumePanel.toggle();
        Debug.Log("volume");
    }

    public void showCredits()
    {
        creditsPanel.SetActive(true);
        Debug.Log("credits");
    }

    public void closeCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void showLevelSelect()
    {
        /*I think this should be
        //a panel so we don't have 
        //to deal with music changing or anytihng.*/
        SceneManager.LoadScene("LevelSelect");
        Debug.Log("levels");
    }

    //public void closeLevelSelect()
    //{
    //    levelSelect.SetActive(false);
    //}

    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }


}
