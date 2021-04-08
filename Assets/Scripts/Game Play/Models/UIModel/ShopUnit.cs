using UnityEngine;
using UnityEngine.UI;

public class ShopUnit : MonoBehaviour
{
    [SerializeField] private Image _profileImage = null;
    [SerializeField] private Text _nameText = null;
    [SerializeField] private Text _priceText = null;
    [SerializeField] private Text _countText = null;

    public void SetShopUnit(Sprite sprite, string name, int price, int count)
    {
        _profileImage.sprite = sprite;
        _nameText.text = name;
        _priceText.text = price.ToString();
        _countText.text = count.ToString();
    }
}
