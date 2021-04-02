using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuySellButton : MonoBehaviour
{
    public GameObject BuyView;
    public GameObject SellView;

    // Start is called before the first frame update
    private void Start()
    {
        BuyView.SetActive(true);
        SellView.SetActive(false);
    }

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
