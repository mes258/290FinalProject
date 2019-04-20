using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private VolumePanel volumePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("First Level");
        Debug.Log("start");
    }

    public void showVolume()
    {
        volumePanel.open();
        Debug.Log("volume");
    }

    public void showCredits()
    {
        Debug.Log("credits");
    }

    public void showLevelSelect()
    {
        /*I think this should be
        //a panel so we don't have 
        //to deal with music changing or anytihng.*/
        Debug.Log("levels");
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }


}
