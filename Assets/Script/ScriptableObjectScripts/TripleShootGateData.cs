using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

[CreateAssetMenu(fileName = "TripleShootGateData", menuName = "Triple Shoot Gate Data")]
public class TripleShootGateData : ScriptableObject
{
    #region Datas
    #region Tweens
    [Header("Destroy Tween")]
    [SerializeField] private float m_DestroyTweenDuration;
    [SerializeField] private Ease m_DestroyTweenEase = Ease.Linear;
    #endregion
    #endregion
    #region ExternalAccess
    public float DestroyTweenDuration => m_DestroyTweenDuration;
    public Ease DestroyTweenEase => m_DestroyTweenEase;
    #endregion
}
