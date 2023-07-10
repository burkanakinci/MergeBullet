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
        GameManager.Instance.UIManager.GetPanel(UIPanelType.RunPanel).ShowPanel();
        m_ShootCounter = 0.0f;
        m_Player.ShootRate = 0.3f;
        m_Player.ShootCount = 1;
        GameManager.Instance.InputManager.OnSwiped += m_Player.SetHorizontalChangeValue;
        GameManager.Instance.LevelManager.StartMissileSpawnCoroutine();
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
        GameManager.Instance.CameraManager.FolowPlayer();
    }
    public void UpdatePhysic()
    {
        m_Player.PlayerForwardMovement();
    }
    public void Exit()
    {
        GameManager.Instance.InputManager.OnSwiped -= m_Player.SetHorizontalChangeValue;
        GameManager.Instance.LevelManager.StopMissileSpawnCoroutine();
        OnExitEvent?.Invoke();
    }
    private IncreaseGate m_TempCollidedIncreaseGate;
    private TripleShootGate m_TempCollidedTripleShootGate;
    private float m_TempDecreaseRateValue;
    public void TriggerEnter(Collider _other)
    {
        if (_other.CompareTag(ObjectTags.TRIPLE_SHOOT_GATE))
        {
            m_TempCollidedTripleShootGate = _other.GetComponent<TripleShootGate>();
            m_TempCollidedTripleShootGate.gameObject.layer = (int)ObjectsLayer.Default;
            m_TempCollidedTripleShootGate.DestroyTripleShootGate();
            m_Player.ShootCount = 3;
        }
        else if (_other.CompareTag(ObjectTags.FIRE_RATE_GATE) && _other.gameObject.layer != (int)ObjectsLayer.Default)
        {
            _other.gameObject.layer = (int)ObjectsLayer.Default;
            m_TempCollidedIncreaseGate = _other.GetComponent<IncreaseGate>();
            m_TempDecreaseRateValue = m_Player.ShootRate / 2.0f;
            m_TempDecreaseRateValue *= (Mathf.Clamp(m_TempCollidedIncreaseGate.GateValue, 0, 100) / 100.0f);
            m_Player.ShootRate -= m_TempDecreaseRateValue;
            m_TempCollidedIncreaseGate.DestroyIncreaseGate();
        }
        else if (_other.CompareTag(ObjectTags.RANGE_GATE) && _other.gameObject.layer != (int)ObjectsLayer.Default)
        {
            _other.gameObject.layer = (int)ObjectsLayer.Default;
            m_TempCollidedIncreaseGate = _other.GetComponent<IncreaseGate>();
            m_TempDecreaseRateValue = m_Player.BulletLifeTime / 2.0f;
            m_TempDecreaseRateValue *= (Mathf.Clamp(m_TempCollidedIncreaseGate.GateValue, 0, 100) / 100.0f);
            m_Player.BulletLifeTime += m_TempDecreaseRateValue;
            m_TempCollidedIncreaseGate.DestroyIncreaseGate();
        }
        else if (_other.CompareTag(ObjectTags.FINISH_TRIGGER))
        {
            GameManager.Instance.LevelSuccess();
        }
        else if (_other.CompareTag(ObjectTags.FINISH_BARREL))
        {
            GameManager.Instance.LevelFailed();
        }
    }
}
