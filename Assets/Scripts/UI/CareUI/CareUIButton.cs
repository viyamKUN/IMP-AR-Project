using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CareUIButton : MonoBehaviour
{
    [SerializeField] private InventoryUnit _inventoryUnit = null;
    [SerializeField] private CareManager careManager;
    [SerializeField] private CareCreatureIndicator indicator;
    private GameObject newItem;
    public bool feedMode = false;
    public void ClickSelect()
    {
        // 아이템 선택 버튼 클릭
        int thisItemId = _inventoryUnit.GetID;
        newItem = careManager.GetItemObject(thisItemId);
        indicator.SetItem(newItem);
        feedMode = true;
        indicator.SetIsFeedMode(feedMode, thisItemId);
    }
}
