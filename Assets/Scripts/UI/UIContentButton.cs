using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContentButton : MonoBehaviour
{
    public GameObject detail;

    private void Start()
    {
        detail.SetActive(false);
    }
    public void ButtonContent()
    {
        detail.SetActive(true);
    }

    public void ButtonExit()
    {
        detail.SetActive(false);
    }

}
