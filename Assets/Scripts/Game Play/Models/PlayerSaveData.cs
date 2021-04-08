using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string UserName;
    public int Money;
    public Dictionary<int, int> MyItems;
    public Dictionary<int, int> MyCreatures;
    public SaveData(string name)
    {
        this.UserName = name.Equals("") ? "Guest" : name;
        this.MyItems = new Dictionary<int, int>();
        this.MyCreatures = new Dictionary<int, int>();
    }
}

public class PlayerSaveData : MonoBehaviour
{
    private SaveData _myData = null;

    #region 플레이어 데이터 Getter
    public string GetPlayerName => _myData.UserName;
    public int GetPlayerMoney => _myData.Money;
    public Dictionary<int, int> GetPlayerItemList => _myData.MyItems;
    public Dictionary<int, int> GetPlayerCreatureList => _myData.MyCreatures;
    #endregion

    public void Init(string name = "")
    {
        _myData = new SaveData(name);

        // 기본 지급
        _myData.Money = 5000;
        _myData.MyItems.Add(0, 1);

        SaveData();
    }
    public void SaveData()
    {
        var filename = SaveFileName.PlayerDataFileName;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(filename);
        binaryFormatter.Serialize(file, _myData);
        file.Close();
    }
    public bool LoadGameData()
    {
        var filename = SaveFileName.PlayerDataFileName;
        if (!File.Exists(filename)) return false;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(filename, FileMode.Open);
        if (file != null && file.Length > 0)
        {
            _myData = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();
            return true;
        }
        return false;
    }
    public void DeleteGameData()
    {
        var filename = SaveFileName.PlayerDataFileName;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(filename);
        Init();
        binaryFormatter.Serialize(file, _myData);
        file.Close();
    }
    public void AddItem(int id, int count)
    {

    }
    public void AddCreature(int id, int count)
    {

    }
}
