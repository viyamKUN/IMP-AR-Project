using UnityEngine;
using UnityEngine.UI;

public class CollectionUnit : MonoBehaviour
{
    [SerializeField] private CollectionDetail _collectionDetail = null;

    [Header("UI components")]
    [SerializeField] private Image _profileImage = null;
    [SerializeField] private Text _nameText = null;
    [SerializeField] private Text _countText = null;
    [SerializeField] private Slider _friendshipSlider = null;
    [SerializeField] private Text _friendshipText = null;
    private int _id = 0;

    public void SetCollectionUnit(int id, Sprite sprite, string name, int count, float friendship)
    {
        _id = id;
        _profileImage.sprite = sprite;
        _nameText.text = name;
        _countText.text = count.ToString();
        _friendshipSlider.value = friendship;
        _friendshipText.text = friendship.ToString() + "%";
    }
    public void ClickMe()
    {
        _collectionDetail.gameObject.SetActive(true);
        _collectionDetail.SetDetailPage(_id);
    }
}
