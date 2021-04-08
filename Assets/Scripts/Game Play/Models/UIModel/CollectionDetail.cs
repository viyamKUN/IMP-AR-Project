using UnityEngine;
using UnityEngine.UI;
using Creatures;

public class CollectionDetail : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    [Header("UI Components")]
    [SerializeField] private Text _name = null;
    [SerializeField] private Image _profile = null;
    [SerializeField] private Text _description = null;
    [SerializeField] private Slider _friendshipSlider = null;
    [SerializeField] private Text _friendshipText = null;
    [SerializeField] private Text _countText = null;
    [SerializeField] private Text _typeText = null;
    [SerializeField] private Text _tallText = null;
    [SerializeField] private Text _weightText = null;
    [SerializeField] private Text _likesText = null;

    public void SetDetailPage(int creatureID)
    {
        Creature creature = _gameManager.GetCreature(creatureID);

        _name.text = creature.Name;
        _profile.sprite = _gameManager.GetCreatureImage(creatureID);
        _description.text = creature.Description;
        _typeText.text = creature.Type;
        _tallText.text = creature.Tall;
        _weightText.text = creature.Weight;
        string favorites = "";
        foreach (int i in creature.FavoriteItemIDs)
        {
            favorites += ", " + _gameManager.GetItem(i).Name;
        }
        _likesText.text = favorites.Remove(0, 2);

        if (_gameManager.GetPlayerSaveData.FindMyCreature(creatureID) < 0)
            return;

        float friendship = _gameManager.GetMyCreature(creatureID).Friendship;
        _friendshipSlider.value = friendship;
        _friendshipText.text = (friendship * 100).ToString() + "%";
        _countText.text = _gameManager.GetMyCreature(creatureID).Count.ToString();
    }
}
