using UnityEngine.EventSystems;
using System;
using UnityEngine;

public class InputManager : CustomBehaviour
{
    #region Attributes
    private bool m_IsUIOverride;
    #endregion
    #region Actions
    public event Action OnTouchDown;
    public event Action<Vector2> OnSwiped;
    public event Action OnTouchUp;
    #endregion
    public override void Initialize()
    {
        m_ScreenWidth = Screen.width;
        m_ScreenHeight = Screen.height;
    }
    public void UpdateInput()
    {
        UpdateUIOverride();
        if (!m_IsUIOverride)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TouchControlsDown();
                OnTouchDown?.Invoke();
            }
            else if (Input.GetMouseButton(0))
            {
                TouchControls();
                OnSwiped?.Invoke(m_InputMovementChange);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                TouchControlsUp();
                OnTouchUp?.Invoke();
            }
        }
    }
    private Vector2 m_TouchDownPos;
    public void TouchControlsDown()
    {
        m_TouchDownPos = Input.mousePosition;
        m_InputMovementChange = Vector2.zero;
    }
    private Vector2 m_TempInputMovementChange;
    private Vector2 m_InputMovementChange;
    private int m_ScreenWidth, m_ScreenHeight;
    private void TouchControls()
    {
        m_InputMovementChange = Vector2.zero;
        m_TempInputMovementChange.x = Input.mousePosition.x - m_TouchDownPos.x;
        m_TempInputMovementChange.y = Input.mousePosition.y - m_TouchDownPos.y;
        m_TempInputMovementChange.x = m_TempInputMovementChange.x / m_ScreenWidth;
        m_TempInputMovementChange.y = m_TempInputMovementChange.y / m_ScreenHeight;
        m_InputMovementChange = m_TempInputMovementChange;
        m_TouchDownPos = Input.mousePosition;

    }
    private void TouchControlsUp()
    {
        m_TempInputMovementChange = Vector2.zero;
        m_InputMovementChange = m_TempInputMovementChange;
    }
    private void UpdateUIOverride()
    {
#if UNITY_EDITOR
        m_IsUIOverride = EventSystem.current.IsPointerOverGameObject();
#else
        m_IsUIOverride = EventSystem.current.IsPointerOverGameObject(0);
#endif
    }
}
