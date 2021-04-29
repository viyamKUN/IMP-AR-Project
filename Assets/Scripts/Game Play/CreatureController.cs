using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CreatureCareController))]
public class CreatureController : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Animator _anim = null;
    public int ID => _id;
    public void Walk()
    {
        _anim.SetBool("isWalk", true);
    }
    public void StopWalking()
    {
        _anim.SetBool("isWalk", false);
    }
    public void Catched()
    {
        Debug.Log("크리쳐를 잡았습니다!");
        Destroy(this.gameObject);
    }
    public void Runaway()
    {
        Debug.Log("크리쳐를 놓쳤습니다.");
        Destroy(this.gameObject);
    }
}
