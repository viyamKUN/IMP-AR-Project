using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CollectionUnit : MonoBehaviour
{
    [SerializeField] private CollectionDetail _collectionDetail = null;

    [Header("UI components")]
    [SerializeField] private Image _profileImage = null;
    [SerializeField] private Text _nameText = null;
    [SerializeField] private Text _countText = null;
    [SerializeField] private Slider _friendshipSlider = null;
    [SerializeField] private Text _friendshipText = null;
    [SerializeField] private GameObject _bagImage = null;
    [SerializeField] private GameObject _friendshipGage = null;
    [SerializeField] private GameObject _joinButton = null;
    private int _id = 0;
    public string SceneName;

    public void SetCollectionUnit(int id, Sprite sprite, string name, int count, float friendship, bool isJoin)
    {
        _id = id;
        _profileImage.sprite = sprite;
        _nameText.text = name;
        _countText.text = count.ToString();
        _friendshipSlider.value = friendship;
        _friendshipText.text = friendship.ToString() + "%";

        _countText.gameObject.SetActive(isJoin);
        _bagImage.SetActive(isJoin);
        _friendshipGage.SetActive(isJoin);
        _joinButton.SetActive(isJoin);
    }
    public void ClickMe()
    {
        _collectionDetail.gameObject.SetActive(true);
        _collectionDetail.SetDetailPage(_id);
        SoundManager.SM.PlaySound(SoundName.BtnPopUp);
    }

    public void InteractScene()
    {
        PlayerPrefs.SetInt(SavePrefName.CareCreatureID, _id);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneName);
    }
}
