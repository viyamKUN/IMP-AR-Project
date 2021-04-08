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
    [SerializeField] private TextAsset _lureItemCsvFile = null;
    [SerializeField] private TextAsset _foodItemCsvFile = null;

    #region For CSV read
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
    #endregion

    public void Read(out List<Creature> _creatureList, out List<Item> _itemData)
    {
        _creatureList = readCreature();
        _itemData = readItem();
    }
    private List<Creature> readCreature()
    {
        var lines = Regex.Split(_creatureCsvFile.text, LINE_SPLIT_RE);
        List<Creature> list = new List<Creature>();

        if (lines.Length <= 1) return null;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            List<int> favoriteItemList = values[3].Split('&').ToList<string>().ConvertAll(int.Parse);
            Creature entry = new Creature(int.Parse(values[0]), values[1], values[2], favoriteItemList, int.Parse(values[4]));
            list.Add(entry);
        }
        return list;
    }
    private List<Item> readItem()
    {
        var lines = Regex.Split(_lureItemCsvFile.text, LINE_SPLIT_RE);
        List<Item> list = new List<Item>();

        if (lines.Length <= 1) return null;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            Item entry = new Item(int.Parse(values[0]), values[1], values[2]);
            list.Add(entry);
        }
        return list;
    }
}
