using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Creatures;
using Items;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager = null;
    [Header("Lobby Only")]
    [SerializeField] private UserInterfaceSetting _userInterfaceSetting = null;
    [SerializeField] private UIUnderButton _uiUnderButton = null;
    [SerializeField] private UIBuySellButton _uiBuySellButton = null;

    [Header("In Game Only")]
    [SerializeField] private InGameBagContentsSetting _inGameBagContentsSetting = null;



    [Header("Values")]
    [SerializeField] private Vector3 _creatureSpawnRange = Vector3.zero;
    [SerializeField] private float _walkSpeed = 1;
    [SerializeField] private float _delayTimeForRunAway = 10;
    [SerializeField] private float _delaySpawnTime = 10;
    [SerializeField] private LayerMask _touchable;


    Vector3 _itemBoxTransform = Vector3.zero;
    GameObject _currentItemObject = null;
    CreatureController _currentCreatureObject = null;
    Coroutine _creatureCoroutine = null;


    private void Awake()
    {
        _dataManager.SetData(out bool isGameDataExist);

        if (_userInterfaceSetting == null)
        {
            if (_inGameBagContentsSetting != null)
            {
                _inGameBagContentsSetting.SetBagContents();
                _inGameBagContentsSetting.CompleteSetting();
            }
            return;
        }

        if (!isGameDataExist)
            _userInterfaceSetting.OpenUserDataEnterPanel();
        else
            SetLobbyWholeUI();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _itemBoxTransform = Vector3.zero;
            // Item 넘버도 임의로 지정
            if (PutItemInBox(1))
            {
                CallCreature(1);
            }
        }
#endif
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit h;
            if (Physics.Raycast(touchRay, out h, Mathf.Infinity, _touchable))
            {
                if (h.transform.gameObject.CompareTag("Creature"))
                {
                    CatchCreature(_currentCreatureObject.ID);
                    return;
                }
            }
        }
    }


    public void CreateUserData(string name)
    {
        _dataManager.CreateNewData(name);
        SetLobbyWholeUI();
    }
    public void SetBoxPosition(Vector3 pos)
    {
        _itemBoxTransform = pos;
        if (_currentItemObject != null)
            _currentItemObject.transform.position = pos;
    }
    public void DeleteBox()
    {
        _itemBoxTransform = Vector3.zero;
    }
    public bool PutItemInBox(int itemID)
    {
        if (_itemBoxTransform == null)
            return false;

        AddItem(itemID, -1);
        _currentItemObject = Instantiate(_dataManager.GetItemModel(itemID), _itemBoxTransform, Quaternion.identity);
        return true;
    }

    /// <summary>지금 설치한 아이템을 좋아하는 크리쳐를 부름</summary>
    public void CallCreature(int itemID)
    {
        Debug.Log("해당 아이템을 선호하는 몬스터를 부르고 있습니다...");

        List<MyCreature> myCreatureList = _dataManager.GetMyCreatureList;
        List<int> tempCreatureList = new List<int>();
        int callCreatureID = 0;

        foreach (Creature c in _dataManager.GetCreatureList)
        {
            if (_dataManager.GetMyCreatureIndex(c.ID) > 0) continue;
            if (c.FavoriteItemIDs.Contains(itemID))
                tempCreatureList.Add(c.ID);
        }

        if (tempCreatureList.Count > 0)
            callCreatureID = tempCreatureList[Random.Range(0, tempCreatureList.Count)];
        else
        {
            tempCreatureList.Clear();

            foreach (var my in myCreatureList)
                if (_dataManager.GetCreature(my.ID).FavoriteItemIDs.Contains(itemID))
                    tempCreatureList.Add(my.ID);

            callCreatureID = Random.Range(0, tempCreatureList.Count);
        }

        Debug.Log(_dataManager.GetCreature(callCreatureID).Name + "이 나타났다!");

        _creatureCoroutine = StartCoroutine(GenerateCreature(callCreatureID));
    }

    /// <summary> 크리쳐를 잡았을 때. 기본적으로 1마리로 취급 </summary>
    public void CatchCreature(int creatureID, int count = 1)
    {
        _dataManager.AddCreature(creatureID, count);

        if (_creatureCoroutine != null)
            StopCoroutine(_creatureCoroutine);

        if (_currentCreatureObject != null)
            _currentCreatureObject.Catched();

        Destroy(_currentItemObject);
    }

    /// <summary> 아이템을 얻었을 때. 기본적으로 1개로 취급 </summary>
    public void AddItem(int itemID, int count = 1)
    {
        _dataManager.AddItem(itemID, count);

        if (_userInterfaceSetting != null)
        {
            _userInterfaceSetting.SetMyProfile(_dataManager.GetPlayerSaveData.GetPlayerName, _dataManager.GetPlayerSaveData.GetPlayerItemList);
            _userInterfaceSetting.SetShop(_dataManager.GetItemList, _dataManager.GetPlayerSaveData.GetPlayerItemList);
        }
        if (_inGameBagContentsSetting != null)
        {
            _inGameBagContentsSetting.SetBagContents();
        }
    }

    public bool CanUseMoney(int payment)
    {
        return _dataManager.CanUseMoney(payment);
    }

    public void AddMoney(int amount)
    {
        _dataManager.AddMoney(amount, out int nowMoney);

        if (_userInterfaceSetting != null)
            _userInterfaceSetting.SetTopUI(nowMoney);
    }

    public bool UseMoney(int payment)
    {
        if (!CanUseMoney(payment)) return false;

        _dataManager.UseMoney(payment, out int nowMoney);

        if (_userInterfaceSetting != null)
            _userInterfaceSetting.SetTopUI(nowMoney);

        return true;
    }

    public int GetItemCount(int itemID)
    {
        return _dataManager.GetItemCount(itemID);
    }

    private void SetLobbyWholeUI()
    {
        if (_userInterfaceSetting == null) return;

        _userInterfaceSetting.SetTopUI(_dataManager.GetPlayerSaveData.PlayerMoney);
        _userInterfaceSetting.SetMyProfile(_dataManager.GetPlayerSaveData.GetPlayerName, _dataManager.GetPlayerSaveData.GetPlayerItemList);
        _userInterfaceSetting.SetMyCollection(_dataManager.GetCreatureList, _dataManager.GetPlayerSaveData.GetPlayerCreatureList);
        _userInterfaceSetting.SetShop(_dataManager.GetItemList, _dataManager.GetPlayerSaveData.GetPlayerItemList);

        _uiUnderButton.CloseWhole();
        _uiUnderButton.ButtonProfile();
        _uiBuySellButton.ButtonBuy();
    }
    private IEnumerator GenerateCreature(int creatureID)
    {
        yield return new WaitForSeconds(_delaySpawnTime);

        Vector3 targetPosition = _currentItemObject.transform.position + _creatureSpawnRange;
        GameObject gameObject = Instantiate(_dataManager.GetCreatureModel(creatureID), targetPosition, _currentItemObject.transform.rotation);
        _currentCreatureObject = gameObject.GetComponent<CreatureController>();
        gameObject.transform.LookAt(_itemBoxTransform);
        Vector3 distance = _currentItemObject.transform.position - gameObject.transform.position;

        _currentCreatureObject.Walk();
        // 걸어오기
        while (true)
        {
            gameObject.transform.position += distance * 0.01f * _walkSpeed;
            yield return new WaitForSeconds(0.001f);

            if (distance.magnitude < 0.005f)
                break;

            distance = _currentItemObject.transform.position - gameObject.transform.position;
        }

        _currentCreatureObject.StopWalking();
        yield return new WaitForSeconds(_delayTimeForRunAway);

        _currentCreatureObject.Runaway();
        Destroy(_currentItemObject);
    }
}
