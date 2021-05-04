using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class CareManager : MonoBehaviour
{
    [SerializeField] private CareUIManager _careUIManager = null;
    [SerializeField] private DataManager _dataManager = null;
    [SerializeField] private LayerMask _touchable;

    CreatureCareController _myCreatureController = null;
    int _thisCreatureID = -1;
    public bool isInteractMode = false; 

    private void init()
    {
        _dataManager.SetData(out bool isGameExist);
        _careUIManager.SetUI();

        if (_thisCreatureID < 0)
            _thisCreatureID = PlayerPrefs.GetInt(SavePrefName.CareCreatureID, 0);
    }
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit h;
            if (Physics.Raycast(touchRay, out h, Mathf.Infinity, _touchable))
            {
                if (h.transform.gameObject.CompareTag("Egg"))
                {
                    _dataManager.AddItem(0, 1);
                    SoundManager.SM.PlaySound(SoundName.GetEgg);
                    Destroy(h.transform.gameObject);
                    return;
                }
                if (!h.transform.gameObject.CompareTag("Creature")) return;

                if (_myCreatureController == null)
                {
                    Debug.Log("크리쳐가 세팅되지 않았습니다.");
                    return;
                }
                if(isInteractMode)
                {
                    _myCreatureController.TouchMe();
                }
            }
        }
    }

    ///<summary>Get creature model for care</summary>
    public GameObject GetCreatureObject()
    {
        init();
        return _dataManager.GetCreatureModel(_thisCreatureID);
    }

    ///<summary>Set creature, When you instantiate the creature.</summary>
    public void SetMyCreature(GameObject go)
    {
        _myCreatureController = go.GetComponent<CreatureCareController>();
        _myCreatureController.CallInit(this);
    }

    ///<summary>Get item model by using id</summary>
    public GameObject GetItemObject(int ID)
    {
        return _dataManager.GetItemModel(ID);
    }
    public void UseItem(int ID, int usingAmount, out int remain)
    {
        SoundManager.SM.PlaySound(SoundName.InstallCandy);
        _dataManager.AddItem(ID, -usingAmount);
        remain = _dataManager.GetItemCount(ID);
        _careUIManager.SetUI();
    }

    public void FeedIt(float friendshipAmount)
    {
        _dataManager.AddFriendship(_myCreatureController.GetCreatureID, friendshipAmount);
        Instantiate(_dataManager.GetItemModel(0), _myCreatureController.gameObject.transform.position, Quaternion.identity);
    }
    public void TouchIt(float friendshipAmount)
    {
        _dataManager.AddFriendship(_myCreatureController.GetCreatureID, friendshipAmount);
    }
}
