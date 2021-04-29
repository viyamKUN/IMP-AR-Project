using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class CareManager : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager = null;
    [SerializeField] private LayerMask _touchable;

    CreatureCareController _myCreatureController = null;
    int _thisCreatureID = -1;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit h;
            if (Physics.Raycast(touchRay, out h, Mathf.Infinity, _touchable))
            {
                if (!h.transform.gameObject.CompareTag("Creature")) return;

                if (_myCreatureController == null)
                {
                    Debug.Log("크리쳐가 세팅되지 않았습니다.");
                    return;
                }
                _myCreatureController.TouchMe();
            }
        }
    }

    ///<summary>Get creature model for care</summary>
    public GameObject GetCreatureObject()
    {
        if (_thisCreatureID < 0)
            _thisCreatureID = PlayerPrefs.GetInt(SavePrefName.CareCreatureID, 0);
        return _dataManager.GetCreatureModel(_thisCreatureID);
    }

    ///<summary>Set creature, When you instantiate the creature.</summary>
    public void SetMyCreature(GameObject go)
    {
        _myCreatureController = go.GetComponent<CreatureCareController>();
        _myCreatureController.CallInit(this);
    }

    public List<Item> GetItemList()
    {
        return _dataManager.GetItemList;
    }
    public void UseItem(int ID, int usingAmount = 1)
    {
        _dataManager.AddItem(ID, -1);
    }

    public void FeedIt(float friendshipAmount)
    {
        _dataManager.AddFriendship(_myCreatureController.GetCreatureID, friendshipAmount);
    }
    public void TouchIt(float friendshipAmount)
    {
        _dataManager.AddFriendship(_myCreatureController.GetCreatureID, friendshipAmount);
    }
}
