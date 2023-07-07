using UnityEngine.EventSystems;
using System;
using UnityEngine;

public class InputManager : CustomBehaviour
{
    #region Attributes
    private bool m_IsUIOverride;
    #endregion
    public override void Initialize()
    {
    }
    public void UpdateUIOverride()
    {

#if UNITY_EDITOR
        m_IsUIOverride = EventSystem.current.IsPointerOverGameObject();
#else
        m_IsUIOverride = EventSystem.current.IsPointerOverGameObject(0);
#endif
    }
}
