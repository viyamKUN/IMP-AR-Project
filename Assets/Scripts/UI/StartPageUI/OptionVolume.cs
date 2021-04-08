using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionVolume : MonoBehaviour
{
    public static OptionVolume instance;
    public AudioMixer mixer;
    public Slider BgmSlider;
    public Text BgmText;
    public Slider SfxSlider;
    public Text SfxText;

    private float BgmSliderValue;
    private float SfxSliderValue;

    void Awake()
    {
        GameObject go = GameObject.Find("SoundManager");

        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(go);
        }
    }
    private void Start()
    {
        BgmSlider.value = PlayerPrefs.GetFloat(SavePrefName.BGM, 0.4f);
        SfxSlider.value = PlayerPrefs.GetFloat(SavePrefName.SUI, 0.4f);

        BGMAudioControl();
        SFXAudioControl();
    }

    public void BGMAudioControl()
    {
        BgmSliderValue = BgmSlider.value;
        mixer.SetFloat("BGM", Mathf.Log10(BgmSliderValue) * 20);
        PlayerPrefs.SetFloat(SavePrefName.BGM, BgmSliderValue);
    }
    public void SFXAudioControl()
    {
        SfxSliderValue = SfxSlider.value;
        mixer.SetFloat("SFX", Mathf.Log10(SfxSliderValue) * 20);
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
