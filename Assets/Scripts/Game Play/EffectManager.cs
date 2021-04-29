using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectName
{
    CATCH, CATCHING, FOOD, HEART, RUN, SPAWN
}
public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _effects = null;
    public void CallEffect(EffectName efName)
    {
        _effects[(int)efName].SetActive(true);
        _effects[(int)efName].GetComponent<ParticleSystem>().Play();
    }
}
