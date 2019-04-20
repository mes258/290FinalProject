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
    private GameObject keyBindings;

    [SerializeField]
    private GameObject volumeControls;

    [SerializeField]
    private Slider MusicVol;

    [SerializeField]
    private Slider SFXVol;

    [SerializeField]
    private AudioMixer mixer;

    private bool settingsEnabled = false;
    private bool showKeyBindings = false;
    private bool showVolume = false;
    // Start is called before the first frame update
    void Start()
    {
        //border = rearViewCameraBorder.GetComponent<Image>();
        //camera = rearViewCamera.GetComponent<RawImage>();

        SettingsCanvas = SettingsGameObj.GetComponent<Canvas>();
        SettingsCanvas.enabled = false;

        LevelEndCanvas = levelEndGameObj.GetComponent<Canvas>();
        LevelEndCanvas.enabled = false;

        disableVolume();
        disableKeybindings();

        SFXVol.onValueChanged.AddListener(delegate { VolChange("SFXVol"); });
        MusicVol.onValueChanged.AddListener(delegate { VolChange("MusicVol"); });

        SFXVol.minValue = 0.001f;
        SFXVol.maxValue = 2;
        float temp = 0;
        mixer.GetFloat("SFXVol", out temp);
        SFXVol.value = DBToSlider(temp); 

        MusicVol.minValue = 0.001f;
        MusicVol.maxValue = 2;
        mixer.GetFloat("MusicVol", out temp);
        MusicVol.value = DBToSlider(temp);
    }

    float sliderToDB(float slider)
    {
        return 12.5182f * Mathf.Log(1.60619f * slider);
    }

    float DBToSlider(float DB)
    {
        return Mathf.Exp(DB / 12.5182f) / 1.60619f;
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
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SettingsCanvas.enabled = false;

            disableVolume();
            disableKeybindings();
        }
    }

    void disableKeybindings()
    {
        keyBindings.SetActive(false);
        showKeyBindings = false;
    }

    void disableVolume()
    {
        volumeControls.SetActive(false);
        showVolume = false;
    }

    public void toggleKeybindings()
    {
        showKeyBindings = !showKeyBindings;
        if (showKeyBindings)
        {
            keyBindings.SetActive(true);
            disableVolume();
        }
        else
        {
            keyBindings.SetActive(false);
        }
    }

    public void toggleVolumeControls()
    {
        showVolume = !showVolume;
        if(showVolume)
        {
            volumeControls.SetActive(true);
            disableKeybindings();
        }
        else
        {
            volumeControls.SetActive(false);
        }
    }

    public void showLevelEnd()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void VolChange(string mixerName)
    {
        float vol = MusicVol.value;
        vol = sliderToDB(vol);
        if (vol < -75f)
        {
            vol = -80f;
        }
        mixer.SetFloat(mixerName, vol);
    }

}
