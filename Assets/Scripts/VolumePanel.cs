using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Slider MusicVol;

    [SerializeField]
    private Slider SFXVol;

    [SerializeField]
    private AudioMixer mixer;

    private Dictionary<string, Slider> sliderNames;

    private bool volumeopen = false;
    // Start is called before the first frame update
    void Start()
    {
        disableVolume();

        if(!PlayerPrefs.HasKey("MusicVol"))
        {
            PlayerPrefs.SetFloat("MusicVol", 1.5f);
        }
        if (!PlayerPrefs.HasKey("SFXVol"))
        {
            PlayerPrefs.SetFloat("SFXVol", 1.5f);
        }

        sliderNames = new Dictionary<string, Slider>() {
            ["SFXVol"] = SFXVol
            ,["MusicVol"] = MusicVol 
        };

        SFXVol.minValue = 0.02f;
        SFXVol.maxValue = 2;
        float initial = PlayerPrefs.GetFloat("SFXVol");
        SFXVol.value = initial;
        VolChange("SFXVol");

        MusicVol.minValue = 0.02f;
        MusicVol.maxValue = 2;
        initial = PlayerPrefs.GetFloat("MusicVol");
        MusicVol.value = initial;
        VolChange("MusicVol");

        SFXVol.onValueChanged.AddListener(delegate { VolChange("SFXVol"); });
        MusicVol.onValueChanged.AddListener(delegate { VolChange("MusicVol"); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void toggle()
    {
        if(volumeopen)
        {
            disableVolume();
        }
        else
        {
            open();
        }
    }
    public void open()
    {
        volumeopen = true;
        panel.gameObject.SetActive(true);
    }

    public void disableVolume()
    {
        volumeopen = false;
        panel.gameObject.SetActive(false);
    }

    public void VolChange(string mixerName)
    {
        float vol = sliderNames[mixerName].value;
        PlayerPrefs.SetFloat(mixerName, vol);
        Debug.Log("Settings " + mixerName + " to " + vol);
        vol = sliderToDB(vol);
        if (vol < -75f)
        {
            vol = -80f;
        }

        if (vol > -4f)
        {
            vol = 0f;
        }
        mixer.SetFloat(mixerName, vol);
    }

    float sliderToDB(float slider)
    {
        //return 12.5182f * Mathf.Log(1.60619f * slider);
        return 18.5489f * Mathf.Log(0.428622f * slider);
        //1.85489 log(0.428622 x)
    }

    float DBToSlider(float DB)
    {
        //return Mathf.Exp(DB / 12.5182f) / 1.60619f;
        return Mathf.Exp(DB / 18.5489f) / 0.428622f;
    }
}
