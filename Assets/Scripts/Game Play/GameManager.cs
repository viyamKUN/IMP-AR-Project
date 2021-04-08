using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Creatures;
using Items;

public class GameManager : MonoBehaviour
{
    [Header("Other Scripts")]
    [SerializeField] private PlayerSaveData _myPlayerSaveData = null;
    [SerializeField] private CsvReader _csvReader = null;
    [SerializeField] private UserInterfaceSetting _userInterfaceSetting = null;
    [SerializeField] private UIUnderButton _uiUnderButton = null;
    [SerializeField] private UIBuySellButton _uiBuySellButton = null;
    [Header("Objects")]
    [SerializeField] private ItemObject[] _itemObjects = null;
    [SerializeField] private CreatureObject[] _creatureObjects = null;
    [Header("Values")]
    [SerializeField] private Vector3 _creatureSpawnRange = Vector3.zero;
    [SerializeField] private float _delayTimeForRunAway = 10;
    [SerializeField] private LayerMask _touchable;
    [SerializeField] private float _saveTimeDelay = 2;

    public PlayerSaveData GetPlayerSaveData => _myPlayerSaveData;
    public Creature GetCreature(int ID) => this._creatureList[ID];
    public MyCreature GetMyCreature(int ID) => this._myPlayerSaveData.GetPlayerCreatureList[ID];
    public Item GetItem(int ID) => this._itemList[ID];
    public Sprite GetItemImage(int ID) => _itemObjects[ID].Profile;
    public Sprite GetCreatureImage(int ID) => _creatureObjects[ID].Profile;

    Transform _itemBoxTransform = null;
    List<Creature> _creatureList = null;
    List<Item> _itemList = null;
    GameObject _currentItemObject = null;
    CreatureController _currentCreatureObject = null;
    Coroutine _creatureCoroutine = null;
    float _timeBucket = 0;


    private void Awake()
    {
        _csvReader.Read(out _creatureList, out _itemList);

        bool isGameDataExist = _myPlayerSaveData.LoadGame();
        if (_userInterfaceSetting == null)
            return;

        if (!isGameDataExist)
            _userInterfaceSetting.OpenUserDataEnterPanel();
        else
            SetUI();
        _timeBucket = Time.time;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _itemBoxTransform = this.transform;
            // Item 넘버도 임의로 지정
            if (PutItemInBox(1))
            {
                CallCreature(1);
            }
        }
#endif
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchPoint = new Vector3(Input.GetTouch(0).deltaPosition.x, Input.GetTouch(0).deltaPosition.y, 0);
            Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit h;
            if (Physics.Raycast(touchRay, out h, _touchable))
            {
                if (h.transform.gameObject.CompareTag("Creature"))
                {
                    CatchCreature(_currentCreatureObject.ID);
                    return;
                }
            }
        }
    }

    /// <summary>게임 세이브</summary>
    public void CallGameSave()
    {
        _myPlayerSaveData.SaveGame();
    }
    public void CreateUserData(string name)
    {
        _myPlayerSaveData.Init(name);
        SetUI();
    }
    public void SetBoxPosition(Transform value)
    {
        _itemBoxTransform = value;
    }
    public bool PutItemInBox(int itemID)
    {
        Vector3 targetPosition = _itemBoxTransform.position;

        _currentItemObject = Instantiate(_itemObjects[itemID].ItemModel, targetPosition, Quaternion.identity);
        return true;
    }

    /// <summary>지금 설치한 아이템을 좋아하는 크리쳐를 부름</summary>
    public void CallCreature(int itemID)
    {
        Debug.Log("해당 아이템을 선호하는 몬스터를 부르고 있습니다...");

        List<MyCreature> myCreatureList = _myPlayerSaveData.GetPlayerCreatureList;
        List<int> tempCreatureList = new List<int>();
        int callCreatureID = 0;

        foreach (Creature c in _creatureList)
        {
            if (_myPlayerSaveData.FindMyCreature(c.ID) > 0) continue;
            if (c.FavoriteItemIDs.Contains(itemID))
                tempCreatureList.Add(c.ID);
        }

        if (tempCreatureList.Count > 0)
            callCreatureID = tempCreatureList[Random.Range(0, tempCreatureList.Count)];
        else
        {
            tempCreatureList.Clear();

            foreach (var my in myCreatureList)
                if (_creatureList[my.ID].FavoriteItemIDs.Contains(itemID))
                    tempCreatureList.Add(my.ID);

            callCreatureID = Random.Range(0, tempCreatureList.Count);
        }

        Debug.Log(_creatureList[callCreatureID].Name + "이 나타났다!");

        _creatureCoroutine = StartCoroutine(GenerateCreature(callCreatureID));
    }

    /// <summary> 크리쳐를 잡았을 때. 기본적으로 1마리로 취급 </summary>
    public void CatchCreature(int creatureID, int count = 1)
    {
        if (_creatureCoroutine != null)
            StopCoroutine(_creatureCoroutine);

        int myCreatureIndex = _myPlayerSaveData.FindMyCreature(creatureID);

        if (myCreatureIndex > 0)
            _myPlayerSaveData.GetPlayerCreatureList[myCreatureIndex].Count += count;
        else
            _myPlayerSaveData.GetPlayerCreatureList.Add(new MyCreature(creatureID, count, 0));

        if (_currentCreatureObject != null)
            _currentCreatureObject.Catched();

        _myPlayerSaveData.SaveGame();
    }

    /// <summary> 아이템을 얻었을 때. 기본적으로 1개로 취급 </summary>
    public void AddItem(int itemID, int count = 1)
    {
        if (_myPlayerSaveData.GetPlayerItemList.ContainsKey(itemID))
            _myPlayerSaveData.GetPlayerItemList[itemID] += count;
        else
            _myPlayerSaveData.GetPlayerItemList.Add(itemID, count);

        _myPlayerSaveData.SaveGame();

        if (_userInterfaceSetting != null)
            _userInterfaceSetting.SetMyProfile(_myPlayerSaveData.GetPlayerName, _myPlayerSaveData.GetPlayerItemList);
    }

    public bool CanUseMoney(int payment)
    {
        if (_myPlayerSaveData.PlayerMoney < payment)
            return false;
        return true;
    }

    public void UseMoney(int payment)
    {
        if (!CanUseMoney(payment)) return;
        _myPlayerSaveData.PlayerMoney -= payment;
        _myPlayerSaveData.SaveGame();

        if (_userInterfaceSetting != null)
            _userInterfaceSetting.SetTopUI(_myPlayerSaveData.PlayerMoney);
    }

    public int GetItemCount(int itemID)
    {
        if (_myPlayerSaveData.GetPlayerItemList.ContainsKey(itemID))
            return _myPlayerSaveData.GetPlayerItemList[itemID];

        return 0;
    }

    private void SetUI()
    {
        if (_userInterfaceSetting == null)
            return;
        _userInterfaceSetting.SetTopUI(_myPlayerSaveData.PlayerMoney);
        _userInterfaceSetting.SetMyProfile(_myPlayerSaveData.GetPlayerName, _myPlayerSaveData.GetPlayerItemList);
        _userInterfaceSetting.SetMyCollection(_creatureList.Count, _creatureList, _myPlayerSaveData.GetPlayerCreatureList);
        _userInterfaceSetting.SetShop(_itemList, _myPlayerSaveData.GetPlayerItemList);

        _uiUnderButton.CloseWhole();
        _uiUnderButton.ButtonProfile();
        _uiBuySellButton.ButtonBuy();
    }
    private IEnumerator GenerateCreature(int creatureID)
    {
        Vector3 targetPosition = _itemBoxTransform.position + _creatureSpawnRange;
        GameObject gameObject = Instantiate(_creatureObjects[creatureID].CreatureModel, targetPosition, Quaternion.identity);
        _currentCreatureObject = gameObject.GetComponent<CreatureController>();
        gameObject.transform.LookAt(_itemBoxTransform.position);

        yield return new WaitForSeconds(_delayTimeForRunAway);

        _currentCreatureObject.Runaway();
        Destroy(_currentItemObject);
    }
}

[System.Serializable]
public struct ItemObject
{
    public string Name;
    public Sprite Profile;
    public GameObject ItemModel;
}
[System.Serializable]
public struct CreatureObject
{
    public string Name;
    public Sprite Profile;
    public GameObject CreatureModel;
}