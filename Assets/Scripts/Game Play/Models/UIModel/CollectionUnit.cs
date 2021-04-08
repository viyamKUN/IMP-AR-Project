using UnityEngine;
using UnityEngine.UI;

public class CollectionUnit : MonoBehaviour
{
    [SerializeField] private Image _profileImage = null;
    [SerializeField] private Text _nameText = null;
    [SerializeField] private Text _countText = null;
    [SerializeField] private Slider _friendshipSlider = null;
    [SerializeField] private Text _friendshipText = null;

    public void SetCollectionUnit(Sprite sprite, string name, int count, float friendship)
    {
        _profileImage.sprite = sprite;
        _nameText.text = name;
        _countText.text = count.ToString();
        _friendshipSlider.value = friendship;
        _friendshipText.text = friendship.ToString() + "%";
    }
}
