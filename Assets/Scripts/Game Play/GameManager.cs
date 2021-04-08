using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerSaveData _myPlayerSaveData = null;
    [SerializeField] private ItemObject[] _itemObjects = null;

    public PlayerSaveData GetPlayerSaveData => _myPlayerSaveData;
    Transform _itemBoxTransform = null;

    private void Awake()
    {
        bool isGameDataExist = _myPlayerSaveData.LoadGameData();
        if (!isGameDataExist)
        {
            // TODO 유저가 유저네임 입력하는 공간 띄워주고 그 데이터로 초기 데이터 생성하게 구현하기~
            _myPlayerSaveData.Init("UserSampleName");
        }
    }
    private void Update()
    {
        // TODO 유저가 상자를 클릭하여 아이템을 선택한 후, 설치함. 지금은 키 입력으로 대체함
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Item 넘버도 임의로 지정
            if (PutItemInBox(0))
            {
                CallCreature(0);
            }
        }
    }
    /// <summary>게임 세이브</summary>
    public void CallGameSave()
    {
        _myPlayerSaveData.SaveData();
    }
    public void SetBoxPosition(Transform value)
    {
        _itemBoxTransform = value;
    }
    public bool PutItemInBox(int itemID)
    {
        Vector3 targetPosition = Vector3.zero;
        if (_itemBoxTransform != null)
            targetPosition = _itemBoxTransform.position;

        GameObject lureitem = Instantiate(_itemObjects[itemID].ItemModel, targetPosition, Quaternion.identity);
        return true;
    }

    /// <summary>지금 설치한 아이템을 좋아하는 크리쳐를 부름</summary>
    public void CallCreature(int itemID)
    {
        Debug.Log("해당 아이템을 선호하는 몬스터를 부르고 있습니다.");
        // TODO
        // 1. 도감에 없는 크리쳐를 우선적으로 탐색 -> 조건에 맞으면 호출
        // 2. 도감에 있는 크리쳐를 탐색 -> 조건에 맞으면 호출
        // 3. 조건에 실패하면 대기
    }

    /// <summary> 크리쳐를 잡았을 때. 기본적으로 1마리로 취급 </summary>
    public void CatchCreature(int creatureID, int count = 1)
    {
        if (_myPlayerSaveData.GetPlayerCreatureList.ContainsKey(creatureID))
        {
            _myPlayerSaveData.GetPlayerCreatureList[creatureID] += count;
        }
        else
        {
            _myPlayerSaveData.GetPlayerCreatureList.Add(creatureID, count);
        }
    }

    /// <summary> 아이템을 얻었을 때. 기본적으로 1개로 취급 </summary>
    public void GetItem(int itemID, int count = 1)
    {
        if (_myPlayerSaveData.GetPlayerItemList.ContainsKey(itemID))
        {
            _myPlayerSaveData.GetPlayerItemList[itemID] += count;
        }
        else
        {
            _myPlayerSaveData.GetPlayerItemList.Add(itemID, count);
        }
    }
}

[System.Serializable]
public struct ItemObject
{
    public string Name;
    public GameObject ItemModel;
}
