using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "IncreaseGateData", menuName = "Increase Gate Data")]
public class IncreaseGateData : ScriptableObject
{
    #region Datas
    [SerializeField] private Color m_PositiveGateColor;
    [SerializeField] private Color m_NegativeGateColor;
    [SerializeField] private Color m_ZeroGateColor;
    #endregion
    #region ExternalAccess
    public Color PositiveGateColor => m_PositiveGateColor;
    public Color NegativeGateColor => m_NegativeGateColor;
    public Color ZeroGateColor => m_ZeroGateColor;
    #endregion
}
