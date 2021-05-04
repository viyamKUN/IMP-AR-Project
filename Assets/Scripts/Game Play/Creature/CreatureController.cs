using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CreatureCareController))]
public class CreatureController : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Animator _anim = null;
    [SerializeField] private EffectManager _efManager = null;
    [SerializeField] private AudioClip _voice = null;
    public int ID => _id;
    public void Walk()
    {
        PlayVoice();
        _anim.SetBool("isWalk", true);
        SoundManager.SM.PlaySound(SoundName.ShockWave);
        _efManager.CallEffect(EffectName.SPAWN);
    }
    public void StopWalking()
    {
        _anim.SetBool("isWalk", false);
    }
    public void Catched()
    {
        PlayVoice();
        StartCoroutine(catchCoroutine());
    }
    public void Runaway()
    {
        PlayVoice();
        StartCoroutine(runawayCoroutine());
    }
    public void PlayVoice()
    {
        SoundManager.SM.PlayClip(_voice);
    }
    IEnumerator catchCoroutine()
    {
        SoundManager.SM.PlaySound(SoundName.Catch);
        _efManager.CallEffect(EffectName.CATCH);
        Debug.Log("크리쳐를 잡았습니다!");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    IEnumerator runawayCoroutine()
    {
        SoundManager.SM.PlaySound(SoundName.Run);
        _efManager.CallEffect(EffectName.RUN);
        Debug.Log("크리쳐를 놓쳤습니다.");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
