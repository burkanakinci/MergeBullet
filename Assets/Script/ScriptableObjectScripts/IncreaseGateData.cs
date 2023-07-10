using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

[CreateAssetMenu(fileName = "IncreaseGateData", menuName = "Increase Gate Data")]
public class IncreaseGateData : ScriptableObject
{
    #region Datas
    #region Colors
    [Header("Gate Colors")]
    [SerializeField] private Color m_PositiveGateColor;
    [SerializeField] private Color m_NegativeGateColor;
    [SerializeField] private Color m_ZeroGateColor;
    #endregion
    #region Tweens
    [Header("Destroy Tween")]
    [SerializeField] private float m_DestroyTweenDuration;
    [SerializeField] private Ease m_DestroyTweenEase = Ease.Linear;

    [Header("Punch Tween")]
    [SerializeField] private float m_PunchScaleUpDuration;
    [SerializeField] private Ease m_PunchScaleUpnEase = Ease.Linear;
    [SerializeField] private float m_PunchScaleDownDuration;
    [SerializeField] private Ease m_PunchScaleDownnEase = Ease.Linear;
    #endregion
    #endregion
    #region ExternalAccess
    public Color PositiveGateColor => m_PositiveGateColor;
    public Color NegativeGateColor => m_NegativeGateColor;
    public Color ZeroGateColor => m_ZeroGateColor;
    public float DestroyTweenDuration => m_DestroyTweenDuration;
    public Ease DestroyTweenEase => m_DestroyTweenEase;
    public float PunchScaleUpDuration => m_PunchScaleUpDuration;
    public Ease PunchScaleUpnEase => m_PunchScaleUpnEase;
    public float PunchScaleDownDuration => m_PunchScaleDownDuration;
    public Ease PunchScaleDownnEase => m_PunchScaleDownnEase;
    #endregion
}
