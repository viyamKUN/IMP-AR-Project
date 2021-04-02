using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBagManager : MonoBehaviour
{
    public GameObject MyBagViewer;
    public void LoadMyBag()
    {
        MyBagViewer.SetActive(true);
    }

    public void ExitMyBag()
    {
        MyBagViewer.SetActive(false);
    }
}
