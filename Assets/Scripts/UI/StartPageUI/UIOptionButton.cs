using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionButton : MonoBehaviour
{
    public GameObject OptionView;

    // Start is called before the first frame update
    void Start()
    {
        OptionView.SetActive(false);
    }

    public void ButtonOption()
    {
        SoundManager.SM.PlaySound(SoundName.BtnGo);
        OptionView.SetActive(true);
    }
    public void ButtonOptionOk()
    {
        SoundManager.SM.PlaySound(SoundName.BtnGo);
        OptionView.SetActive(false);
    }
}
