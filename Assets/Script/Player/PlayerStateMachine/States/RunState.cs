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
    private IncreaseGate m_TempCollidedGate;
    private float m_TempDecreaseRateValue;
    public void TriggerEnter(Collider _other)
    {
        if (_other.CompareTag(ObjectTags.TRIPLE_SHOOT_GATE))
        {
            _other.gameObject.layer = (int)ObjectsLayer.Default;
            m_Player.ShootCount = 3;
        }
        else if (_other.CompareTag(ObjectTags.FIRE_RATE_GATE) && _other.gameObject.layer != (int)ObjectsLayer.Default)
        {
            _other.gameObject.layer = (int)ObjectsLayer.Default;
            m_TempCollidedGate = _other.GetComponent<IncreaseGate>();
            m_TempDecreaseRateValue = m_Player.ShootRate / 2.0f;
            m_TempDecreaseRateValue *= (Mathf.Clamp(m_TempCollidedGate.GateValue, 0, 100) / 100.0f);
            m_Player.ShootRate -= m_TempDecreaseRateValue;
        }
        else if (_other.CompareTag(ObjectTags.RANGE_GATE) && _other.gameObject.layer != (int)ObjectsLayer.Default)
        {
            _other.gameObject.layer = (int)ObjectsLayer.Default;
            m_TempCollidedGate = _other.GetComponent<IncreaseGate>();
            m_TempDecreaseRateValue = m_Player.BulletLifeTime / 2.0f;
            m_TempDecreaseRateValue *= (Mathf.Clamp(m_TempCollidedGate.GateValue, 0, 100) / 100.0f);
            m_Player.BulletLifeTime += m_TempDecreaseRateValue;
            Debug.Log(m_Player.BulletLifeTime);
        }
    }
}
