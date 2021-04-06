using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

using Creatures;
using Items;

public class CsvReader : MonoBehaviour
{
    [Header("CSV Files")]
    [SerializeField] private TextAsset _creatureCsvFile = null;
    [SerializeField] private TextAsset _itemCsvFile = null;

    #region  Lists
    List<Creature> _creatureData = new List<Creature>();
    List<Item> _itemData = new List<Item>();
    #endregion

    #region For CSV read
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
    #endregion

    private void Start()
    {
        readCreature();
        readItem();
    }
    private bool readCreature()
    {
        var lines = Regex.Split(_creatureCsvFile.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return false;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            List<int> favoriteItemList = values[3].Split('&').ToList<string>().ConvertAll(int.Parse);
            Creature entry = new Creature(int.Parse(values[0]), values[1], values[2], favoriteItemList, int.Parse(values[4]));
            _creatureData.Add(entry);
        }
        return true;
    }
    private bool readItem()
    {
        var lines = Regex.Split(_creatureCsvFile.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return false;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            Item entry = new Item(int.Parse(values[0]), values[1], values[2]);
            _itemData.Add(entry);
        }
        return true;
    }
}
