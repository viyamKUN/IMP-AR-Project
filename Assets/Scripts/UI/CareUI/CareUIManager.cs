using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;

public class CareUIManager : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager = null;
    [SerializeField] private GameObject _careItemsParent = null;
    InventoryUnit[] _careItemsUnits;

    public void SetUI()
    {
        if (_careItemsUnits == null)
            _careItemsUnits = _careItemsParent.GetComponentsInChildren<InventoryUnit>();

        List<Item> tempList = _dataManager.GetItemList;
        int unitPin = 0;

        foreach (InventoryUnit go in _careItemsUnits)
        {
            go.gameObject.SetActive(false);
        }

        for (int i = 0; i < tempList.Count; i++)
        {
            if (!_dataManager.IsContainItem(i))
                continue;

            _careItemsUnits[unitPin].gameObject.SetActive(true);
            _careItemsUnits[unitPin++].SetInventoryUnit(
                i,
                _dataManager.GetItemImage(i),
                _dataManager.GetItem(i).Name,
                _dataManager.GetItemCount(i)
            );
        }
    }
}
