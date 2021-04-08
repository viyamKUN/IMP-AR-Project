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

    [Header("Collection Page")]
    [SerializeField] private Slider _collectionPercent = null;
    [SerializeField] private Text _collectionPercentText = null;
    [SerializeField] private GameObject _catchedParent = null;
    [SerializeField] private GameObject _nonCatchParent = null;
    CollectionUnit[] _catched = null;
    CollectionUnit[] _nonCatch = null;

    [Header("Shop Page")]
    [SerializeField] private GameObject _shopBuyFoodParent = null;
    [SerializeField] private GameObject _shopBuyCatchParent = null;
    [SerializeField] private GameObject _shopSellFoodParent = null;
    [SerializeField] private GameObject _shopSellCatchParent = null;
    ShopUnit[] _shopBuyFoods = null;
    ShopUnit[] _shopBuyCatchs = null;
    ShopUnit[] _shopSellFoods = null;
    ShopUnit[] _shopSellCatchs = null;


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
        if (_foodItems == null) _foodItems = _foodItemsParent.GetComponentsInChildren<InventoryUnit>();
        if (_catchItems == null) _catchItems = _catchItemsParent.GetComponentsInChildren<InventoryUnit>();

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
        }
    }

    public void SetMyCollection(int countAll, List<Creature> wholeCollection, List<MyCreature> myCollections)
    {
        float percent = (float)myCollections.Count / (float)countAll;
        _collectionPercent.value = percent;
        _collectionPercentText.text = (percent * 100).ToString() + "%";

        if (_catched == null) _catched = _catchedParent.GetComponentsInChildren<CollectionUnit>();
        if (_nonCatch == null) _nonCatch = _nonCatchParent.GetComponentsInChildren<CollectionUnit>();

        int catchPointer = 0;
        int nonCatchPointer = 0;

        foreach (CollectionUnit g in _catched)
            g.gameObject.SetActive(false);
        foreach (CollectionUnit g in _nonCatch)
            g.gameObject.SetActive(false);

        foreach (var item in wholeCollection)
        {
            int myCreatureIndex = _gameManager.GetPlayerSaveData.FindMyCreature(item.ID);
            if (myCreatureIndex > 0)
            {
                _catched[catchPointer].gameObject.SetActive(true);
                _catched[catchPointer].SetCollectionUnit(
                    _gameManager.GetCreatureImage(item.ID),
                    item.Name,
                    myCollections[myCreatureIndex].Count,
                    myCollections[myCreatureIndex].Friendship
                );
                catchPointer++;
            }
            else
            {
                _nonCatch[nonCatchPointer].gameObject.SetActive(true);
                _nonCatch[nonCatchPointer].SetCollectionUnit(_gameManager.GetCreatureImage(item.ID), item.Name, 0, 0);
                nonCatchPointer++;
            }
        }
    }

    public void SetShop(List<Item> itemList, Dictionary<int, int> myItem)
    {
        if (_shopBuyFoods == null) _shopBuyFoods = _shopBuyFoodParent.GetComponentsInChildren<ShopUnit>();
        if (_shopBuyCatchs == null) _shopBuyCatchs = _shopBuyCatchParent.GetComponentsInChildren<ShopUnit>();
        if (_shopSellFoods == null) _shopSellFoods = _shopSellFoodParent.GetComponentsInChildren<ShopUnit>();
        if (_shopSellCatchs == null) _shopSellCatchs = _shopSellCatchParent.GetComponentsInChildren<ShopUnit>();

        int foodPointer = 0;
        int catchPointer = 0;

        foreach (ShopUnit g in _shopBuyFoods)
            g.gameObject.SetActive(false);
        foreach (ShopUnit g in _shopBuyCatchs)
            g.gameObject.SetActive(false);

        foreach (var item in itemList)
        {
            if (item.MyType.Equals(ItemType.Food))
            {
                _shopBuyFoods[foodPointer].gameObject.SetActive(true);
                _shopBuyFoods[foodPointer].SetShopUnit(_gameManager.GetItemImage(item.ID), item.Name, item.Price, _gameManager.GetItemCount(item.ID));
                foodPointer++;
            }
            else if (item.MyType.Equals(ItemType.Catch))
            {
                _shopBuyCatchs[catchPointer].gameObject.SetActive(true);
                _shopBuyCatchs[catchPointer].SetShopUnit(_gameManager.GetItemImage(item.ID), item.Name, item.Price, _gameManager.GetItemCount(item.ID));
                catchPointer++;
            }
        }


        foodPointer = 0;
        catchPointer = 0;

        foreach (ShopUnit g in _shopSellFoods)
            g.gameObject.SetActive(false);
        foreach (ShopUnit g in _shopSellCatchs)
            g.gameObject.SetActive(false);

        foreach (var item in itemList)
        {
            if (myItem.ContainsKey(item.ID))
            {
                if (item.MyType.Equals(ItemType.Food))
                {
                    _shopSellFoods[foodPointer].gameObject.SetActive(true);
                    _shopSellFoods[foodPointer].SetShopUnit(_gameManager.GetItemImage(item.ID), item.Name, item.Price, _gameManager.GetItemCount(item.ID));
                    foodPointer++;
                }
                else if (item.MyType.Equals(ItemType.Catch))
                {
                    _shopSellCatchs[catchPointer].gameObject.SetActive(true);
                    _shopSellCatchs[catchPointer].SetShopUnit(_gameManager.GetItemImage(item.ID), item.Name, item.Price, _gameManager.GetItemCount(item.ID));
                    catchPointer++;
                }
            }
        }
    }
}
