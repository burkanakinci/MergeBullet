using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MergingPlatform : CustomBehaviour
{
    public MergingGrid MergingGrid;
    private List<MergingBullet> m_MergingBullets;
    public event Action OnShootMergingBullets;
    #region ExternalAccess
    public int MerginBulletCount => m_MergingBullets.Count;
    #endregion
    public override void Initialize()
    {
        MergingGrid.Initialize(this);
        m_MergingBullets = new List<MergingBullet>();
    }
    public void SetMergingBulletList(ListOperations _operation, MergingBullet _merginBullet)
    {
        switch (_operation)
        {
            case ListOperations.Adding:
                if (!m_MergingBullets.Contains(_merginBullet))
                {
                    m_MergingBullets.Add(_merginBullet);
                }
                break;
            case ListOperations.Removing:
                if (m_MergingBullets.Contains(_merginBullet))
                {
                    m_MergingBullets.Remove(_merginBullet);
                }
                break;
        }
    }
    public void ShootMergingBullets()
    {
        OnShootMergingBullets?.Invoke();
    }
    public void ForwardMergingBullets()
    {
        for (int _mergingCount = 0; _mergingCount < m_MergingBullets.Count; _mergingCount++)
        {
            m_MergingBullets[_mergingCount].SetVelocity();
        }
    }
    private float m_AverageZ;
    public float GetAverageMergingBullets()
    {
        m_AverageZ = 0.0f;
        for (int _mergingCount = 0; _mergingCount < m_MergingBullets.Count; _mergingCount++)
        {
            m_AverageZ += m_MergingBullets[_mergingCount].transform.position.z;
        }
        m_AverageZ /= m_MergingBullets.Count;
        return m_AverageZ;
    }
}
