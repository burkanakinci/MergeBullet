using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
public class PlayerManager : CustomBehaviour
{
    #region Fields
    public Player Player;
    #endregion
    public override void Initialize()
    {
        Player.Initialize(this);

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnGameStart += OnGameStart;
        GameManager.Instance.OnLevelSuccess += OnLevelSuccess;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;
    }
    #region Events
    public void OnResetToMainMenu()
    {
        Player.PlayerStateMachine.ChangeStateTo(PlayerStates.MergingState, true);
    }
    public void OnGameStart()
    {
        Player.PlayerStateMachine.ChangeStateTo(PlayerStates.RunState);
    }
    public void OnLevelSuccess()
    {
        Player.PlayerStateMachine.ChangeStateTo(PlayerStates.WinState);
    }
    public void OnLevelFailed()
    {
        Player.PlayerStateMachine.ChangeStateTo(PlayerStates.FailState);
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnGameStart -= OnGameStart;
        GameManager.Instance.OnLevelSuccess -= OnLevelSuccess;
        GameManager.Instance.OnLevelFailed -= OnLevelFailed;
    }
    #endregion
}
