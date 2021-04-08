using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider BgmSlider;
    public Text BgmText;
    public Slider SfxSlider;
    public Text SfxText;

    private float BgmSliderValue;
    private float SfxSliderValue;

    void Start()
    {
        BgmSlider.value = PlayerPrefs.GetFloat(SavePrefName.BGM, 0.75f);
        SfxSlider.value = PlayerPrefs.GetFloat(SavePrefName.SUI, 0.75f);
    }

    public void BGMAudioControl()
    {
        BgmSliderValue = BgmSlider.value;
        mixer.SetFloat(SavePrefName.BGM, Mathf.Log10(BgmSliderValue) * 20);
        PlayerPrefs.SetFloat(SavePrefName.BGM, BgmSliderValue);
    }
    public void SFXAudioControl()
    {
        SfxSliderValue = SfxSlider.value;
        mixer.SetFloat(SavePrefName.SUI, Mathf.Log10(SfxSliderValue) * 20);
        PlayerPrefs.SetFloat(SavePrefName.SUI, SfxSliderValue);
    }

    public void BGMUpdateTextObject()
    {
        BgmText.text = Mathf.Round(BgmSliderValue * 100).ToString();
    }

    public void SFXUpdateTextObject()
    {
        SfxText.text = Mathf.Round(SfxSliderValue * 100).ToString();
    }
}
