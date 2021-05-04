using UnityEngine;
using Items;

public class InGameBagContentsSetting : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private DataManager _dataManager = null;
    [SerializeField] private MyBagManager _myBagManager = null;
    [SerializeField] private GameObject _bagContentsParent = null;
    private BagContent[] _bagContents = null;
    public void SetBagContents()
    {
        if (_bagContents == null) _bagContents = _bagContentsParent.GetComponentsInChildren<BagContent>();

        int i = 0;

        foreach (BagContent b in _bagContents)
        {
            if (_gameManager.GetItemCount(i) > 0)
            {
                if (_dataManager.GetItem(i).MyType.Equals(ItemType.Catch))
                {
                    b.SetBagContent(i, _dataManager.GetItemImage(i), _dataManager.GetItem(i).Name, _dataManager.GetItemCount(i));
                    i++;
                    continue;
                }
            }
            b.gameObject.SetActive(false);
            i++;
        }
    }
    public void CompleteSetting()
    {
        _myBagManager.ExitMyBag(false);
    }
}
