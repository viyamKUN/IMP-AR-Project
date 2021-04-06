using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using Creatures;
using System.Linq;

public class CsvReader : MonoBehaviour
{
    [SerializeField] private TextAsset _creatureCsvFile = null;
    [SerializeField] private TextAsset _itemCsvFile = null;
    List<Creature> _creatureData = new List<Creature>();
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    private void Start()
    {
        ReadCreature();
    }
    private bool ReadCreature()
    {
        var list = new List<Creature>();

        var lines = Regex.Split(_creatureCsvFile.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return false;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            List<int> favoriteItemList = values[3].Split('&').ToList<string>().ConvertAll(int.Parse);
            Creature entry = new Creature(int.Parse(values[0]), values[1], values[2], favoriteItemList, int.Parse(values[4]));
            list.Add(entry);
        }
        _creatureData = list;
        return true;
    }
}
