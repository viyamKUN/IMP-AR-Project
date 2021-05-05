using UnityEngine;
using UnityEngine.UI;
using Creatures;

public class CollectionDetail : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager = null;
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
        Creature creature = _dataManager.GetCreature(creatureID);

        _name.text = creature.Name;
        _profile.sprite = _dataManager.GetCreatureImage(creatureID);
        _description.text = creature.Description.Replace('|', ',');
        _typeText.text = creature.Type;
        _tallText.text = creature.Tall;
        _weightText.text = creature.Weight;
        string favorites = "";
        foreach (int i in creature.FavoriteItemIDs)
        {
            favorites += ", " + _dataManager.GetItem(i).Name;
        }
        _likesText.text = favorites.Remove(0, 2);

        if (_dataManager.GetMyCreatureIndex(creatureID) < 0)
            return;

        float friendship = _dataManager.GetMyCreature(creatureID).Friendship;
        _friendshipSlider.value = friendship;
        _friendshipText.text = System.String.Format("{0:0.00}", friendship * 100) + "%";
        _countText.text = _dataManager.GetMyCreature(creatureID).Count.ToString();
    }
}
