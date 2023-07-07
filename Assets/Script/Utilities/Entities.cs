using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
public class Entities : CustomBehaviour
{

    [Header("Hierarchy Objects")]
    [SerializeField] private Transform[] m_DeactiveParents;
    [SerializeField] private Transform[] m_ActiveParents;

    [Header("Scene Objects")]
    [SerializeField] private MergingPlatform m_MergingPlatform;

    public override void Initialize()
    {
        m_MergingPlatform.Initialize();
    }
    #region  Getter
    public Transform GetActiveParent(ActiveParents _activeParent)
    {
        return m_ActiveParents[(int)_activeParent];
    }
    public Transform GetDeactiveParent(DeactiveParents _deactiveParent)
    {
        return m_DeactiveParents[(int)_deactiveParent];
    }
    #endregion
}