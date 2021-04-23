using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCareController : MonoBehaviour
{
    private CareManager _careManager = null;
    public void CallInit(CareManager careManager)
    {
        if (_careManager != null) return;
        this._careManager = careManager;
    }
    public void TouchMe()
    {
        Debug.Log("She touched me.");
    }
    private void FeedMe()
    {
        Debug.Log("She feed me.");
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Food")) return;
        FeedMe();
        _careManager.FeedIt();
    }
}
