using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    [SerializeField] private int _id;
    public int ID => _id;
    public void Catched()
    {
        Debug.Log("크리쳐를 잡았습니다!");
    }
    public void Runaway()
    {
        Debug.Log("크리쳐를 놓쳤습니다.");
        Destroy(this.gameObject);
    }
}
