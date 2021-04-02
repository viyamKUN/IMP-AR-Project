using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISellButton : MonoBehaviour
{
    public GameObject SellMessage;

    private void Start()
    {
        SellMessage.SetActive(false);
    }

    public void ButtonContent()
    {
        SellMessage.SetActive(true);
    }

    public void ButtonYes()
    {
        SellMessage.SetActive(false);
        Debug.Log("Yes");
    }

    public void ButtonNo()
    {
        SellMessage.SetActive(false);
        Debug.Log("No");
    }

}
