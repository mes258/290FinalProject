using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIControl : MonoBehaviour
{
    //[SerializeField]
    //private GameObject rearViewCamera;

    //[SerializeField]
    //private GameObject rearViewCameraBorder;

    [SerializeField]
    private GameObject levelEndGameObj;
    private Canvas LevelEndCanvas;

    [SerializeField]
    private GameObject SettingsGameObj;
    private Canvas SettingsCanvas;

    [SerializeField]
    private GameObject menuObj;

    [SerializeField]
    private GameObject keyBindings;

    [SerializeField]
    private VolumePanel volumePanel;

    [SerializeField]
    private GameObject eventSystem;

    private bool settingsEnabled = false;
    private bool showKeyBindings = false;
    private bool showEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        //border = rearViewCameraBorder.GetComponent<Image>();
        //camera = rearViewCamera.GetComponent<RawImage>();

        SettingsCanvas = SettingsGameObj.GetComponent<Canvas>();
        SettingsCanvas.enabled = false;

        LevelEndCanvas = levelEndGameObj.GetComponent<Canvas>();
        LevelEndCanvas.enabled = false;

        volumePanel.disableVolume();
        disableKeybindings();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleSettings();
        }
    }

    public void toggleSettings()
    {
        settingsEnabled = !settingsEnabled;
        if(settingsEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SettingsCanvas.enabled = true;
            eventSystem.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SettingsCanvas.enabled = false;

            volumePanel.disableVolume();
            disableKeybindings();
            if (!showEnd)
            {
                eventSystem.SetActive(false);
            }
        }
    }

    public void showVolume()
    {
        volumePanel.open();
    }

    void disableKeybindings()
    {
        keyBindings.SetActive(false);
        showKeyBindings = false;
    }



    public void toggleKeybindings()
    {
        showKeyBindings = !showKeyBindings;
        if (showKeyBindings)
        {
            keyBindings.SetActive(true);
            volumePanel.disableVolume();
            menuObj.SetActive(false);
        }
        else
        {
            keyBindings.SetActive(false);
            menuObj.SetActive(true);
        }
    }

   

    public void showLevelEnd()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        eventSystem.SetActive(true);
        showEnd = true;
        Debug.Log("Level over");
        LevelEndCanvas.enabled = true;
    }

    public void goToNextLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
