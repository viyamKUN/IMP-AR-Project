using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBagManager : MonoBehaviour
{
    public GameObject MyBagViewer;
    public void LoadMyBag()
    {
        SoundManager.SM.PlaySound(SoundName.BtnGo);
        MyBagViewer.SetActive(true);
    }

    public void ExitMyBag(bool play)
    {
        if (play)
            SoundManager.SM.PlaySound(SoundName.BtnExit);
        MyBagViewer.SetActive(false);
    }
}
