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
        // Food Item
        // Catch Item
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
