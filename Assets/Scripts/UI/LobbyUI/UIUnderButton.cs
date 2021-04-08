using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnderButton : MonoBehaviour
{
    public GameObject profile;
    public GameObject book;
    public GameObject shop;

    private void Start()
    {
        profile.SetActive(true);
        book.SetActive(false);
        shop.SetActive(false);
    }
    public void ButtonProfile()
    {
        profile.SetActive(true);
        book.SetActive(false);
        shop.SetActive(false);
    }
    public void ButtonBook()
    {
        profile.SetActive(false);
        book.SetActive(true);
        shop.SetActive(false);
    }
    public void ButtonShop()
    {
        profile.SetActive(false);
        book.SetActive(false);
        shop.SetActive(true);
    }
}
