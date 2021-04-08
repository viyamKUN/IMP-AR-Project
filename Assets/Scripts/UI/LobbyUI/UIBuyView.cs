using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyView : MonoBehaviour
{
    public GameObject BuyMessage;

    public Text CountText;
    public Text MyCountText;
    public Text TotalGoldText;

    public int Gold = 100;
    public int MyCount = 12;

    private int Count = 0;
    private int TotalGold = 0;

    private void Awake()
    {
        BuyMessage.SetActive(false);
    }

    //buy window open
    public void ButtonContent()
    {
        BuyMessage.SetActive(true);
        CountText.text = "0";
        TotalGoldText.text = "0";
        MyCountText.text = MyCount.ToString();
        Count = 0;
    }

    //select amount
    public void PlusButton()
    {
        if (Count+MyCount < 99)
        {
            Count++;
            TotalGold = Gold * Count;
            CountText.text = Count.ToString();
            TotalGoldText.text = TotalGold.ToString();
        }
    }
    public void MinusButton()
    {
        if (Count > 0)
        {
            Count--;
            TotalGold = Gold * Count;
            CountText.text = Count.ToString();
            TotalGoldText.text = TotalGold.ToString();
        }
    }

    //yes or no
    public void ButtonYes()
    {
        BuyMessage.SetActive(false);
        Debug.Log("Yes");
        MyCount += Count;
        Debug.Log("Now My Item Number is: " + MyCount);
    }
    public void ButtonNo()
    {
        BuyMessage.SetActive(false);
        Debug.Log("No");
        Debug.Log("Now My Item Number is: " + MyCount);
    }
}
