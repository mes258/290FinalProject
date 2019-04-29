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

        }
    }

    public void continueGame()
    {
        int level = 1;
        int score = HighScoreLogger.LookupScore(level);
        while (score != -1)
        {
            level++;
            score = HighScoreLogger.LookupScore(level);

            if(level == 10)
            {
                level = 1;
                break;
            }
        }

        playLevel("Level" + level);
    }

    public void startGame()
    {
        playLevel("Level1");
    }

    public void playLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void showVolume()
    {
        volumePanel.toggle();
    }

    public void showCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void closeCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void showLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void showScoreCard()
    {
        SceneManager.LoadScene("ScoreCard");
    }

    //public void closeLevelSelect()
    //{
    //    levelSelect.SetActive(false);
    //}

    public void quit()
    {
        Application.Quit();
    }


}
