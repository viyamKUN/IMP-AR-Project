using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CreatureCareController))]
public class CreatureController : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Animator _anim = null;
    [SerializeField] private EffectManager _efManager = null;
    public int ID => _id;
    public void Walk()
    {
        _anim.SetBool("isWalk", true);
        _efManager.CallEffect(EffectName.SPAWN);
    }
    public void StopWalking()
    {
        _anim.SetBool("isWalk", false);
    }
    public void Catched()
    {
        StartCoroutine(catchCoroutine());
    }
    public void Runaway()
    {
        StartCoroutine(runawayCoroutine());
    }
    IEnumerator catchCoroutine()
    {
        _efManager.CallEffect(EffectName.CATCH);
        Debug.Log("크리쳐를 잡았습니다!");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    IEnumerator runawayCoroutine()
    {
        _efManager.CallEffect(EffectName.RUN);
        Debug.Log("크리쳐를 놓쳤습니다.");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
