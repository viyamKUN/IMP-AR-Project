using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuySellButton : MonoBehaviour
{
    public GameObject BuyView;
    public GameObject SellView;

    public void ButtonBuy()
    {
        BuyView.SetActive(true);
        SellView.SetActive(false);
    }
    public void ButtonSell()
    {
        BuyView.SetActive(false);
        SellView.SetActive(true);
    }
}
