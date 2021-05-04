using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundName
{
    BtnExit, BtnGo, BtnPopUp, Catch, Eat, GetEgg, Heart, InstallCandy, InstallItem, Run, SelectItem, ShockWave, ShopCoin
}
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audio = null;
    [SerializeField] private AudioClip[] _clips = null;
    public static SoundManager SM;
    private void Awake()
    {
        SM = this;
    }
    public void PlaySound(SoundName sName)
    {
        _audio.PlayOneShot(_clips[(int)sName]);
    }
}
