using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCareController : MonoBehaviour
{
    [SerializeField] private CareManager _careManager = null;
    [SerializeField] private DataManager _dataManager = null;

    public void TouchMe()
    {
        Debug.Log("She touched me.");
    }
    public void FeedMe()
    {
        Debug.Log("She feed me");
    }
}
