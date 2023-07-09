using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Player Movement Data")]
public class PlayerMovementData : ScriptableObject
{
    #region Datas
    [SerializeField] private float m_PlayerForwardSpeed;
    [SerializeField] private float m_PlayerHorizontalSpeed;
    #endregion
    #region ExternalAccess
    public float PlayerForwardSpeed => m_PlayerForwardSpeed;
    public float PlayerHorizontalSpeed => m_PlayerHorizontalSpeed;
    #endregion
}
