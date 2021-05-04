using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Creatures;
using Items;

public class DataManager : MonoBehaviour
{
    [Header("Other Scripts")]
    [SerializeField] private PlayerSaveData _myPlayerSaveData = null;
    [SerializeField] private CsvReader _csvReader = null;
    [Header("Objects")]
    [SerializeField] private ItemObject[] _itemObjects = null;
    [SerializeField] private CreatureObject[] _creatureObjects = null;
    [SerializeField] private float _saveTimeDelay = 2;

    public PlayerSaveData GetPlayerSaveData => _myPlayerSaveData;

    public Item GetItem(int ID) => this._itemList[ID];
    public List<Item> GetItemList => _itemList;
    public GameObject GetItemModel(int ID) => _itemObjects[ID].ItemModel;
    public Sprite GetItemImage(int ID) => _itemObjects[ID].Profile;
    public bool IsContainItem(int ID) => _myPlayerSaveData.GetPlayerItemList.ContainsKey(ID);

    public List<Creature> GetCreatureList => _creatureList;
    public Creature GetCreature(int ID) => this._creatureList[ID];
    public GameObject GetCreatureModel(int ID) => _creatureObjects[ID].CreatureModel;
    public Sprite GetCreatureImage(int ID) => _creatureObjects[ID].Profile;

    public List<MyCreature> GetMyCreatureList => _myPlayerSaveData.GetPlayerCreatureList;
    public MyCreature GetMyCreature(int ID) => this._myPlayerSaveData.GetPlayerCreatureList[ID];
    public int GetMyCreatureIndex(int ID) => _myPlayerSaveData.FindMyCreature(ID);

    List<Creature> _creatureList = null;
    List<Item> _itemList = null;
    float _timeBucket = 0;

    public void SetData(out bool isGameDataExist)
    {
        _csvReader.Read(out _creatureList, out _itemList);
        _timeBucket = Time.time;

        isGameDataExist = _myPlayerSaveData.LoadGame();
    }

    /// <summary>게임 세이브</summary>
    private void callGameSave()
    {
        _myPlayerSaveData.SaveGame();
    }
    public void CreateNewData(string name)
    {
        _myPlayerSaveData.Init(name);
    }
    public bool CanUseMoney(int payment)
    {
        if (_myPlayerSaveData.PlayerMoney < payment)
            return false;
        return true;
    }

    public void AddMoney(int amount, out int nowMoney)
    {
        _myPlayerSaveData.PlayerMoney += amount;
        callGameSave();
        nowMoney = _myPlayerSaveData.PlayerMoney;
    }

    public void UseMoney(int payment, out int nowMoney)
    {
        _myPlayerSaveData.PlayerMoney -= payment;
        callGameSave();
        nowMoney = _myPlayerSaveData.PlayerMoney;
    }
    public void AddItem(int itemID, int count = 1)
    {
        if (IsContainItem(itemID))
            _myPlayerSaveData.GetPlayerItemList[itemID] += count;
        else
            _myPlayerSaveData.GetPlayerItemList.Add(itemID, count);

        if (count < 0)
        {
            if (_myPlayerSaveData.GetItemCount(itemID) == 0)
                _myPlayerSaveData.GetPlayerItemList.Remove(itemID);
        }

        callGameSave();
    }
    public int GetItemCount(int itemID)
    {
        if (_myPlayerSaveData.GetPlayerItemList.ContainsKey(itemID))
            return _myPlayerSaveData.GetItemCount(itemID);

        return 0;
    }
    public void AddCreature(int creatureID, int count)
    {
        int myCreatureIndex = GetMyCreatureIndex(creatureID);

        if (myCreatureIndex >= 0)
            _myPlayerSaveData.GetPlayerCreatureList[myCreatureIndex].Count += count;
        else
            _myPlayerSaveData.GetPlayerCreatureList.Add(new MyCreature(creatureID, count, 0));

        callGameSave();
    }

    ///<summary>Friendship's range is 0 ~ 1</summary>
    public void AddFriendship(int creatureID, float amount)
    {
        int myCreatureIndex = GetMyCreatureIndex(creatureID);
        _myPlayerSaveData.GetPlayerCreatureList[myCreatureIndex].Friendship += amount;
        callGameSave();
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
