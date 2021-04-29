using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;

public class CareUIManager : MonoBehaviour
{
    [SerializeField] private CareManager _careManager = null;
    [SerializeField] private GameObject _careItemsParent = null;
    InventoryUnit[] _careItemsUnits;

    void Start()
    {
        _careItemsUnits = _careItemsParent.GetComponentsInChildren<InventoryUnit>();
        initUI();
    }
    private void initUI()
    {
        List<Item> tempList = _careManager.GetItemList();
        for (int i = 0; i < tempList.Count; i++)
        {

        }
    }
    public void RefreshUI()
    {
        // 카운트 바꾸기
        // 사용한 것 없애기
    }
}
