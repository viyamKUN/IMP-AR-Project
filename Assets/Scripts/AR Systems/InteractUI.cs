using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private GameObject feedTab;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject backButton;
    [SerializeField] private CareManager careManager;

    private void Start()
    {
        buttons.SetActive(true);
        feedTab.SetActive(false);
        background.SetActive(true);
        backButton.SetActive(false);
    }

    public void FeedMode()
    {
        careManager.IsOnMode = InteractableMode.Feed;
        SoundManager.SM.PlaySound(SoundName.BtnGo);
        buttons.SetActive(false);
        feedTab.SetActive(true);
        background.SetActive(true);
        backButton.SetActive(true);
    }

    public void InteractMode()
    {
        careManager.IsOnMode = InteractableMode.Touch;
        SoundManager.SM.PlaySound(SoundName.BtnGo);
        buttons.SetActive(false);
        feedTab.SetActive(false);
        background.SetActive(true);
        backButton.SetActive(true);
    }

    public void BackButton()
    {
        careManager.IsOnMode = InteractableMode.Off;
        SoundManager.SM.PlaySound(SoundName.BtnExit);
        buttons.SetActive(true);
        backButton.SetActive(false);
        feedTab.SetActive(false);
    }
}
