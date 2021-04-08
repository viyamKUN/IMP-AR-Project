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
        OptionView.SetActive(true);
    }
    public void ButtonOptionOk()
    {
        OptionView.SetActive(false);
    }
}
