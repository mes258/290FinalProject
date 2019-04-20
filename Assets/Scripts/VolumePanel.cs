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

    // Start is called before the first frame update
    void Start()
    {
        disableVolume();

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
        float vol = MusicVol.value;
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
