using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private GameObject feedTab;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject background;
    
    private void Start() 
    {
        Debug.Log("start");    
    }
    public void FeedMode()
    {
        Debug.Log("feed1");
        buttons.SetActive(false);
        feedTab.SetActive(true);
        background.SetActive(false);
        Debug.Log("feed");
    }

    public void InteractMode()
    {
        buttons.SetActive(false);
        feedTab.SetActive(false);
        background.SetActive(true);
    }
}
