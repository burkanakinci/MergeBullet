using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BulletMovementData", menuName = "Bullet Movement Data")]
public class BulletMovementData : ScriptableObject
{
    #region Datas
    [SerializeField] private float m_Speed;
    #endregion
    #region ExternalAccess
    public float BulletSpeed => m_Speed;
    #endregion
}
