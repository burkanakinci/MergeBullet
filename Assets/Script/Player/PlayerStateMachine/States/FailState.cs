﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FailState : IPlayerState
{
    public Action OnEnterEvent { get; set; }
    public Action OnExitEvent { get; set; }
    private Player m_Player;
    public FailState(Player _player)
    {
        m_Player = _player;
    }

    public void Enter()
    {
        GameManager.Instance.UIManager.GetPanel(UIPanelType.FinishPanel).ShowPanel();
        GameManager.Instance.UIManager.CurrentUIPanel.HideAllArea();
        GameManager.Instance.UIManager.CurrentUIPanel.ShowArea<FinishAreaType>(FinishAreaType.FailArea);
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
