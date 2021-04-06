using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveUnit
{
    public int ID;
    public int Count;
}

[System.Serializable]
public class SaveData
{
    public string UserName;
    public List<SaveUnit> MyItems;
    public List<SaveUnit> MyCreatures;
    public SaveData(string name)
    {
        UserName = name.Equals("") ? "Guest" : name;
        MyItems = new List<SaveUnit>();
        MyCreatures = new List<SaveUnit>();
    }
}

public class PlayerSaveData : MonoBehaviour
{
    private SaveData _myData = null;
    public void Init(string name = "")
    {
        _myData = new SaveData(name);
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
}
