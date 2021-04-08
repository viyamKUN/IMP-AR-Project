using UnityEngine;
using UnityEngine.UI;

public class InventoryUnit : MonoBehaviour
{
    [SerializeField] private Image _profileImage = null;
    [SerializeField] private Text _nameText = null;
    [SerializeField] private Text _countText = null;

    public void SetInventoryUnit(Sprite sprite, string name, int count)
    {
        _profileImage.sprite = sprite;
        _nameText.text = name;
        _countText.text = count.ToString();
    }
}
