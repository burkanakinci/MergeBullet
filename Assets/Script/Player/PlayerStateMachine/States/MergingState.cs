using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MergingState : IPlayerState
{
    public Action OnEnterEvent { get; set; }
    public Action OnExitEvent { get; set; }
    private Player m_Player;
    public MergingState(Player _player)
    {
        m_Player = _player;
    }

    public void Enter()
    {
        GameManager.Instance.LevelManager.SetLevelNumber(m_Player.PlayerLevel);
        GameManager.Instance.LevelManager.CreateLevel();
        GameManager.Instance.UIManager.GetPanel(UIPanelType.MergingPanel).ShowPanel();
        GameManager.Instance.InputManager.OnTouchDown += InputDownMerging;
        GameManager.Instance.InputManager.OnTouchUp += InputUp;
        OnEnterEvent?.Invoke();
    }
    private Action m_RayCollidedEvent;
    private RaycastHit m_MergingHit;
    private Ray m_MergingRay;
    private int m_MergingLayerMask;
    private MergingBullet m_ClickedMergingBullet;
    private void MergingRay()
    {
        m_MergingRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(m_MergingRay, out m_MergingHit, 1000f, m_MergingLayerMask))
        {
            m_RayCollidedEvent?.Invoke();
        }
    }
    private void InputDownMerging()
    {
        m_MergingLayerMask = 1 << (int)ObjectsLayer.MergingBullet;
        m_RayCollidedEvent = () =>
        {
            m_ClickedMergingBullet = m_MergingHit.transform.GetComponent<MergingBullet>();
            m_ClickedMergingBullet.CLickedMergingBullet();
            m_RayCollidedEvent = () =>
            {
                SwipeMergingBullet();
            };
            m_MergingLayerMask = 1 << (int)ObjectsLayer.Road;
        };
    }
    private void SwipeMergingBullet()
    {
        if (m_ClickedMergingBullet != null)
        {
            m_ClickedMergingBullet.transform.position = new Vector3(m_MergingHit.point.x, m_ClickedMergingBullet.transform.position.y, m_MergingHit.point.z);
        }
    }
    private void InputUp()
    {
        m_MergingLayerMask = -1;
        if (m_ClickedMergingBullet != null)
        {
            m_ClickedMergingBullet.ClickUpMergingBullet();
            m_ClickedMergingBullet = null; 
        }
    }
    public void UpdateLogic()
    {
        GameManager.Instance.InputManager.UpdateInput();
        MergingRay();
    }
    public void UpdatePhysic()
    {

    }
    public void Exit()
    {
        GameManager.Instance.InputManager.OnTouchDown -= InputDownMerging;
        GameManager.Instance.InputManager.OnTouchUp -= InputUp;
        OnExitEvent?.Invoke();
    }
    public void TriggerEnter(Collider _other)
    {
    }
}
