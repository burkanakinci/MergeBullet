using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "GunData", menuName = "Gun Data")]
public class GunData : ScriptableObject
{
    #region Datas
    [Header("On Merging State Complete")]
    [SerializeField] private float m_DestroyTweenDuration;
    [SerializeField] private Ease m_DestroyTweenEase = Ease.Linear;
    [SerializeField] private float m_LocalMoveDuration;
    [SerializeField] private Ease m_LocalMoveEase;
    #endregion
    #region ExternalAccess 
    public float DestroyTweenDuration => m_DestroyTweenDuration;
    public Ease DestroyTweenEase => m_DestroyTweenEase;
    public float LocalMoveDuration => m_LocalMoveDuration;
    public Ease LocalMoveEase => m_LocalMoveEase;
    #endregion
}
