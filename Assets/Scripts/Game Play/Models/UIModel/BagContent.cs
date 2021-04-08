using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagContent : MonoBehaviour
{
    [SerializeField] private Image _profileImage = null;
    [SerializeField] private Text _itemName = null;
    [SerializeField] private Text _itemCount = null;
    [SerializeField] private Button _itemSelectButton = null;
    [SerializeField] private GameManager gameManager;
    int _id = 0;
    public void SetBagContent(int id, Sprite sprite, string name, int count)
    {
        this._id = id;
        this._profileImage.sprite = sprite;
        this._itemName.text = name;
        this._itemCount.text = count.ToString();
    }
    public void SelectButton()
    {
        Debug.Log("ID: " + _id + " 를 클릭함");
        gameManager.PutItemInBox(_id);
    }
}
