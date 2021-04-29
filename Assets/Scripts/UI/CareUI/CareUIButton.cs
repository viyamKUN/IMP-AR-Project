using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CareUIButton : MonoBehaviour
{
    [SerializeField] private InventoryUnit _inventoryUnit = null;
    public void ClickSelect()
    {
        // 아이템 선택 버튼 클릭
        int thisItemId = _inventoryUnit.GetID;
    }
}
