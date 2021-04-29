using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private GameObject feedTab;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject background;
    
    public void FeedMode()
    {
        buttons.SetActive(false);
        feedTab.SetActive(true);
        background.SetActive(true);
    }

    public void InteractMode()
    {
        buttons.SetActive(false);
        feedTab.SetActive(false);
        background.SetActive(true);
    }
}
