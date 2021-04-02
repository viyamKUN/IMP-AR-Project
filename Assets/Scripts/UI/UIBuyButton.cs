using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuyButton : MonoBehaviour
{
    public GameObject BuyMessage;

    private void Start()
    {
        BuyMessage.SetActive(false);
    }

    public void ButtonContent()
    {
        BuyMessage.SetActive(true);
    }

    public void ButtonYes()
    {
        BuyMessage.SetActive(false);
        Debug.Log("Yes");
    }

    public void ButtonNo()
    {
        BuyMessage.SetActive(false);
        Debug.Log("No");
    }

}
