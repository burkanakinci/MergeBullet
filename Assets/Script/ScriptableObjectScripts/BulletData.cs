using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BulletData", menuName = "Bullet Data")]
public class BulletData : ScriptableObject
{
    #region Datas
    [SerializeField] private int m_BulletDamage;
    #endregion
    #region ExternalAccess
    public int BulletDamage => m_BulletDamage;
    #endregion
}
