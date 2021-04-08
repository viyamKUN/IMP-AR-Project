using UnityEngine;
using Items;

public class InGameBagContentsSetting : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
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
                if (_gameManager.GetItem(i).MyType.Equals(ItemType.Catch))
                {
                    b.SetBagContent(i, _gameManager.GetItemImage(i), _gameManager.GetItem(i).Name, _gameManager.GetPlayerSaveData.GetItemCount(i));
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
        _myBagManager.ExitMyBag();
    }
}
