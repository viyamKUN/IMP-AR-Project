using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CareManager : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager = null;
    [SerializeField] private LayerMask _touchable;

    CreatureCareController _myCreatureController = null;


    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit h;
            if (Physics.Raycast(touchRay, out h, Mathf.Infinity, _touchable))
            {
                if (!h.transform.gameObject.CompareTag("Creature")) return;

                if (_myCreatureController == null)
                {
                    Debug.Log("크리쳐가 세팅되지 않았습니다.");
                    return;
                }
                _myCreatureController.TouchMe();
            }
        }
    }
    ///<summary>Set creature, When you instantiate the creature.</summary>
    public void SetMyCreature(GameObject go)
    {
        _myCreatureController = go.GetComponent<CreatureCareController>();
    }

    public void UseItem(int ID, int usingAmount = 1)
    {

    }

    public void FeedIt()
    {
        // TODO 크리쳐에게 음식물을 먹인 후의 데이터 관리
        // ex 호감도 증가
    }
}
