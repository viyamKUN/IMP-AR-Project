using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerSaveData _myPlayerSaveData = null;
    private void Awake()
    {
        bool isGameDataExist = _myPlayerSaveData.LoadGameData();
        if (!isGameDataExist)
        {
            // TODO 유저가 유저네임 입력하는 공간 띄워주고 그 데이터로 초기 데이터 생성하게 구현하기~
            _myPlayerSaveData.Init("UserSampleName");
        }
    }
}
