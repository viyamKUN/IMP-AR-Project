using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnderButton : MonoBehaviour
{
    public GameObject profile;
    public GameObject book;
    public GameObject shop;
    public void CloseWhole()
    {
        profile.SetActive(true);
        book.SetActive(false);
        shop.SetActive(false);
    }
    public void ButtonProfile()
    {
        SoundManager.SM.PlaySound(SoundName.BtnGo);
        profile.SetActive(true);
        book.SetActive(false);
        shop.SetActive(false);
    }
    public void ButtonBook()
    {
        SoundManager.SM.PlaySound(SoundName.BtnGo);
        profile.SetActive(false);
        book.SetActive(true);
        shop.SetActive(false);
    }
    public void ButtonShop()
    {
        SoundManager.SM.PlaySound(SoundName.BtnGo);
        profile.SetActive(false);
        book.SetActive(false);
        shop.SetActive(true);
    }
}
