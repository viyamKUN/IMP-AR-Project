using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCareController : MonoBehaviour
{
    [SerializeField] private CreatureController _creature = null;
    [Header("Care Values")]
    [SerializeField] private float _feedCareValue = 0.05f;
    [SerializeField] private float _touchCareValue = 0.01f;
    private CareManager _careManager = null;

    public int GetCreatureID => _creature.ID;

    public void CallInit(CareManager careManager)
    {
        if (_careManager != null) return;
        this._careManager = careManager;
    }

    public void TouchMe()
    {
        Debug.Log("She touched me.");
        _careManager.TouchIt(_touchCareValue);
    }

    private void FeedMe()
    {
        Debug.Log("She feed me.");
        _careManager.FeedIt(_feedCareValue);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Food")) return;
        FeedMe();
    }
}