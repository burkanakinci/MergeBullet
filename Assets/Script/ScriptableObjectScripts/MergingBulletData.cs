using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

[CreateAssetMenu(fileName = "MergingBulletData", menuName = "Merging Bullet Data")]
public class MergingBulletData : ScriptableObject
{
    #region Datas
    [Header("Clicked Tween")]
    [SerializeField] private float m_ClickedScaleDuration;
    [SerializeField] private Ease m_ClickedScaleEase = Ease.Linear;
    [SerializeField] private float m_ClickedScaleMultiplier;
    [SerializeField] private float m_ClickedMovementDuration;
    [SerializeField] private Ease m_ClickedMovementEase = Ease.Linear;
    [SerializeField] private float m_ClickedHeight;

    [Header("Clicked Up Tween")]
    [SerializeField] private float m_ClickedUpMovementDuration;
    [SerializeField] private Ease m_ClickedUpMovementEase = Ease.Linear;
    #endregion
    #region ExternalAccess
    public float ClickedScaleDuration => m_ClickedScaleDuration;
    public Ease ClickedScaleEase => m_ClickedScaleEase;
    public float ClickedScaleMultiplier => m_ClickedScaleMultiplier;
    public float ClickedMovementDuration => m_ClickedMovementDuration;
    public Ease ClickedMovementEase => m_ClickedMovementEase;
    public float ClickedHeight => m_ClickedHeight;
    public float ClickedUpMovementDuration => m_ClickedUpMovementDuration;
    public Ease ClickedUpMovementEase => m_ClickedUpMovementEase;
    #endregion
}
