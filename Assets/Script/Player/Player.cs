using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : CustomBehaviour<PlayerManager>
{
    private PlayerData m_PlayerData;
    #region ExternalAccess
    public int PlayerLevel => m_PlayerData.PlayerLevel;
    public int PlayerCoin => m_PlayerData.PlayerCoin;
    public List<Node> PlayerFilledNodes => m_PlayerData.FilledNodes;
    #endregion
    public PlayerStateMachine PlayerStateMachine { get; private set; }
    public override void Initialize(PlayerManager _playerManager)
    {
        GameManager.Instance.JsonConverter.LoadPlayerData(ref m_PlayerData);
        PlayerStateMachine = new PlayerStateMachine(this);
    }
    public void SetPlayerLevel(int _level)
    {
        m_PlayerData.PlayerLevel = _level;
        GameManager.Instance.JsonConverter.SavePlayerData(m_PlayerData);
    }
    private void Update()
    {
        PlayerStateMachine.LogicalUpdate();
    }
}
