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
        GameManager.Instance.UIManager.SetCurrentUIPanel(UIPanelType.MergingPanel);
        OnEnterEvent?.Invoke();
    }
    public void UpdateLogic()
    {

    }
    public void UpdatePhysic()
    {

    }
    public void Exit()
    {
        OnExitEvent?.Invoke();
    }
    public void TriggerEnter(Collider _other)
    {
    }
}
