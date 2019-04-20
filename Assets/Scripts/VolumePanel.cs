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
    // Start is called before the first frame update
    void Start()
    {
        disableVolume();

        if(!PlayerPrefs.HasKey("MusicVol"))
        {
            PlayerPrefs.SetFloat("MusicVol", 1.5f);
        }
        else
        {
            Debug.Log("Found Key");
            Debug.Log(PlayerPrefs.GetFloat("MusicVol"));
        }
        if (!PlayerPrefs.HasKey("SFXVol"))
        {
            PlayerPrefs.SetFloat("SFXVol", 1.5f);
        }
        else
        {
            Debug.Log("Found Key");
            Debug.Log(PlayerPrefs.GetFloat("SFXVol"));
        }

        sliderNames = new Dictionary<string, Slider>() {
            ["SFXVol"] = SFXVol
            ,["MusicVol"] = MusicVol 
        };

        SFXVol.minValue = 0.001f;
        SFXVol.maxValue = 2;
        float initial = PlayerPrefs.GetFloat("SFXVol");
        SFXVol.value = initial;
        VolChange("SFXVol");

        MusicVol.minValue = 0.001f;
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

    public void open()
    {
        panel.gameObject.SetActive(true);
    }

    public void disableVolume()
    {
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
        mixer.SetFloat(mixerName, vol);
    }

    float sliderToDB(float slider)
    {
        return 12.5182f * Mathf.Log(1.60619f * slider);
    }

    float DBToSlider(float DB)
    {
        return Mathf.Exp(DB / 12.5182f) / 1.60619f;
    }
}
