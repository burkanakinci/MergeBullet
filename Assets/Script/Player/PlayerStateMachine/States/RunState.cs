using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RunState : IPlayerState
{
    public Action OnEnterEvent { get; set; }
    public Action OnExitEvent { get; set; }
    private Player m_Player;
    public RunState(Player _player)
    {
        m_Player = _player;
    }

    public void Enter()
    {
        m_ShootCounter = 0.0f;
        m_Player.ShootRate = 1.0f;
        m_Player.ShootCount = 1;
        GameManager.Instance.InputManager.OnSwiped += m_Player.SetHorizontalChangeValue;
        OnEnterEvent?.Invoke();
    }
    private float m_ShootCounter;
    public void UpdateLogic()
    {
        if (m_ShootCounter < m_Player.ShootRate)
        {
            m_ShootCounter += Time.deltaTime;
        }
        else
        {
            m_Player.Shoot();
            m_ShootCounter = 0.0f;
        }
        GameManager.Instance.InputManager.UpdateInput();
    }
    public void UpdatePhysic()
    {
        m_Player.PlayerForwardMovement();
    }
    public void Exit()
    {
        GameManager.Instance.InputManager.OnSwiped -= m_Player.SetHorizontalChangeValue;
        OnExitEvent?.Invoke();
    }
    public void TriggerEnter(Collider _other)
    {
        Debug.Log(_other.tag);
    }
}
