using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Creatures;
using Items;

public class UserInterfaceSetting : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    [Header("Top")]
    [SerializeField] private Text _myMoneyText = null;

    [Header("User Data Enter Field")]
    [SerializeField] private GameObject _userDataEnterPanel = null;
    [SerializeField] private InputField _userNameInputField = null;
    [SerializeField] private Text _supportText = null;
    [SerializeField] private Button _userDataEnterButton = null;

    [Header("MyPages")]
    [SerializeField] private Text _userName = null;
    [SerializeField] private GameObject _foodItemsParent = null;
    [SerializeField] private GameObject _catchItemsParent = null;
    InventoryUnit[] _foodItems = null;
    InventoryUnit[] _catchItems = null;


    #region User Data Enter
    public void OpenUserDataEnterPanel()
    {
        _userDataEnterPanel.gameObject.SetActive(true);
    }
    public void UpdateUserDataEnterField()
    {
        _userDataEnterButton.enabled = _userNameInputField.text.Length > 1;
        _supportText.gameObject.SetActive(_userNameInputField.text.Length <= 2);
    }
    public void EnterUserData()
    {
        _gameManager.CreateUserData(_userNameInputField.text);
        _userDataEnterPanel.gameObject.SetActive(false);
    }
    #endregion

    public void SetTopUI(int money)
    {
        _myMoneyText.text = money.ToString();
    }

    public void SetMyProfile(string name, Dictionary<int, int> myItems)
    {
        _userName.text = name;
        if (_foodItems == null)
            _foodItems = _foodItemsParent.GetComponentsInChildren<InventoryUnit>();

        if (_catchItems == null)
            _catchItems = _catchItemsParent.GetComponentsInChildren<InventoryUnit>();

        int foodPointer = 0;
        int catchPointer = 0;
        Item current;

        foreach (InventoryUnit g in _foodItems)
            g.gameObject.SetActive(false);
        foreach (InventoryUnit g in _catchItems)
            g.gameObject.SetActive(false);

        foreach (var item in myItems)
        {
            current = _gameManager.GetItem(item.Key);
            if (current.MyType.Equals(ItemType.Food))
            {
                _foodItems[foodPointer].gameObject.SetActive(true);
                _foodItems[foodPointer].SetInventoryUnit(_gameManager.GetItemImage(item.Key), current.Name, item.Value);
                foodPointer++;
            }
            else if (current.MyType.Equals(ItemType.Catch))
            {
                _catchItems[catchPointer].gameObject.SetActive(true);
                _catchItems[catchPointer].SetInventoryUnit(_gameManager.GetItemImage(item.Key), current.Name, item.Value);
                catchPointer++;
            }
            else
            {
                // NONE 타입일때 필요함
            }
        }
    }

    public void SetMyCollection()
    {
        // Percent
        // Got
        // NOT get
    }

    public void SetShop()
    {

    }
}
