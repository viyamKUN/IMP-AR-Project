using UnityEngine;
using UnityEngine.UI;
using Items;

public class UIBuyView : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private GameObject _buyMessage;

    [Header("Original Informations")]
    [SerializeField] private Image _itemImage = null;
    [SerializeField] private Text _itemText = null;
    [SerializeField] private Text _itemPriceText = null;
    [SerializeField] private Text _myCountText = null;

    [Header("Change Informations")]
    [SerializeField] private Text _countText = null;
    [SerializeField] private Text _totalGoldText = null;
    [SerializeField] private Button _buyButton = null;

    private int _itemID = 0;
    private int _gold = 0;
    private int _myCount = 0;
    private int _count = 1;

    private void Awake()
    {
        _buyMessage.SetActive(false);
    }

    //buy window open
    public void ButtonContent(int id)
    {
        _buyMessage.SetActive(true);
        _myCount = _gameManager.GetItemCount(id);
        _itemID = id;
        _count = 1;
        _gold = _gameManager.GetItem(id).Price;
        _itemPriceText.text = _gold.ToString();

        _itemImage.sprite = _gameManager.GetItemImage(id);
        _itemText.text = _gameManager.GetItem(id).Name;
        _myCountText.text = _myCount.ToString();

        _countText.text = _count.ToString();
        _totalGoldText.text = (_gold * _count).ToString();
        setBuyButtonActive();
    }

    //select amount
    public void PlusButton()
    {
        if (_count + _myCount >= 99) return;

        _count++;
        _countText.text = _count.ToString();
        _totalGoldText.text = (_gold * _count).ToString();
        setBuyButtonActive();
    }
    public void MinusButton()
    {
        if (_count <= 0) return;

        _count--;
        _countText.text = _count.ToString();
        _totalGoldText.text = (_gold * _count).ToString();
        setBuyButtonActive();
    }

    //yes or no
    public void ButtonYes()
    {
        _buyMessage.SetActive(false);
        int pay = _gold * _count;
        if (_gameManager.CanUseMoney(pay))
        {
            _gameManager.AddItem(_itemID, _count);
            _gameManager.UseMoney(pay);
        }
    }
    public void ButtonNo()
    {
        _buyMessage.SetActive(false);
    }

    private void setBuyButtonActive()
    {
        int pay = _gold * _count;
        _buyButton.interactable = _gameManager.CanUseMoney(pay);
    }
}
