using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreatureAnnotation : MonoBehaviour
{
    [SerializeField] private TextMeshPro _nameText = null;
    [SerializeField] private TextMeshPro _descriptionText = null;
    public void SetAnnotation(string name, string description)
    {
        _nameText.text = name;
        _descriptionText.text = description;
    }
}
