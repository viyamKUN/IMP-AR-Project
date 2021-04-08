using UnityEngine;
using UnityEngine.UI;

public class ShopUnit : MonoBehaviour
{
    [SerializeField] private UIBuyView _uiBuyView = null;
    [SerializeField] private Image _profileImage = null;
    [SerializeField] private Text _nameText = null;
    [SerializeField] private Text _priceText = null;
    [SerializeField] private Text _countText = null;
    private int _id;

    public void SetShopUnit(int id, Sprite sprite, string name, int price, int count)
    {
        _id = id;
        _profileImage.sprite = sprite;
        _nameText.text = name;
        _priceText.text = price.ToString();
        _countText.text = count.ToString();
    }
    public void ClickMe()
    {
        _uiBuyView.ButtonContent(_id);
    }
}
