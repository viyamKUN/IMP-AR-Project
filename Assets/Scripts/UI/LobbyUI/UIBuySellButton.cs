using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuySellButton : MonoBehaviour
{
    public GameObject BuyView;
    public GameObject SellView;

    public void ButtonBuy(bool playSound = true)
    {
        if (playSound)
            SoundManager.SM.PlaySound(SoundName.BtnGo);
        BuyView.SetActive(true);
        SellView.SetActive(false);
    }
    public void ButtonSell()
    {
        SoundManager.SM.PlaySound(SoundName.BtnGo);
        BuyView.SetActive(false);
        SellView.SetActive(true);
    }
}
