using UnityEngine;
using UnityEngine.UI;

public class InventoryUnit : MonoBehaviour
{
    [SerializeField] private Image _profileImage = null;
    [SerializeField] private Text _nameText = null;
    [SerializeField] private Text _countText = null;
    private int _id = 0;
    public int GetID => _id;
    public void SetInventoryUnit(int id, Sprite sprite, string name, int count)
    {
        _id = id;
        _profileImage.sprite = sprite;
        _nameText.text = name;
        _countText.text = count.ToString();
    }
}
