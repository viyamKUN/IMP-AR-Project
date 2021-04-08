using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using Creatures;

[System.Serializable]
public class SaveData
{
    public string UserName;
    public int Money;
    public Dictionary<int, int> MyItems;
    public List<MyCreature> MyCreatures;
    public SaveData(string name)
    {
        this.UserName = name.Equals("") ? "Guest" : name;
        this.MyItems = new Dictionary<int, int>();
        this.MyCreatures = new List<MyCreature>();
    }
}

public class PlayerSaveData : MonoBehaviour
{
    private SaveData _myData = null;

    #region 플레이어 데이터 Getter
    public string GetPlayerName => _myData.UserName;
    public int PlayerMoney { get => _myData.Money; set => _myData.Money = value; }
    public Dictionary<int, int> GetPlayerItemList => _myData.MyItems;
    public List<MyCreature> GetPlayerCreatureList => _myData.MyCreatures;
    public int GetItemCount(int ID) => _myData.MyItems[ID];
    #endregion

    public void Init(string name = "")
    {
        _myData = new SaveData(name);

        // 기본 지급
        _myData.Money = 300;
        _myData.MyItems.Add(1, 1);
        _myData.MyCreatures.Add(new MyCreature(0, 1, 0));

        SaveGame();
    }
    public void SaveGame()
    {
        var filename = SaveFileName.PlayerDataFileName;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(filename);
        binaryFormatter.Serialize(file, _myData);
        file.Close();
    }
    public bool LoadGame()
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
    public void DeleteGame()
    {
        var filename = SaveFileName.PlayerDataFileName;
        File.Delete(filename);
    }
    public int FindMyCreature(int ID)
    {
        int i = 0;
        foreach (MyCreature m in _myData.MyCreatures)
        {
            if (m.ID == ID)
                return i;
            i++;
        }
        return -1;
    }
}
