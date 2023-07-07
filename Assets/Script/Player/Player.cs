using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : CustomBehaviour<PlayerManager>
{
    private PlayerData m_PlayerData;
    #region ExternalAccess
    public Dictionary<Node, int> PlayerFilledNodes;
    public int PlayerLevel => m_PlayerData.PlayerLevel;
    public int PlayerCoin => m_PlayerData.PlayerCoin;
    #endregion
    public PlayerStateMachine PlayerStateMachine { get; private set; }
    #region Event
    public event Action OnChangedCoinValue;
    #endregion
    public override void Initialize(PlayerManager _playerManager)
    {
        GameManager.Instance.JsonConverter.LoadPlayerData(ref m_PlayerData);
        PlayerStateMachine = new PlayerStateMachine(this);
        PlayerFilledNodes = new Dictionary<Node, int>();
    }
    public void SetPlayerCoin(int _coin)
    {
        m_PlayerData.PlayerCoin = _coin;
        GameManager.Instance.JsonConverter.SavePlayerData(m_PlayerData);
        OnChangedCoinValue?.Invoke();
    }
    public void SetPlayerLevel(int _level)
    {
        m_PlayerData.PlayerLevel = _level;
        GameManager.Instance.JsonConverter.SavePlayerData(m_PlayerData);
    }
    public void AddPlayerFilledNodes(Node _node, int _level)
    {
        m_PlayerData.FilledNodesX.Add(_node.NodeXIndis);
        m_PlayerData.FilledNodesY.Add(_node.NodeYIndis);
        m_PlayerData.FilledNodesLevel.Add(_level);
        GameManager.Instance.JsonConverter.SavePlayerData(m_PlayerData);
    }
    private int m_TempRemoveIndex;
    public void RemovePlayerFilledNodes(Node _node)
    {
        for (int _filledCount = 0; _filledCount < m_PlayerData.FilledNodesLevel.Count; _filledCount++)
        {
            if (m_PlayerData.FilledNodesX[_filledCount] == _node.NodeXIndis && m_PlayerData.FilledNodesY[_filledCount] == _node.NodeYIndis)
            {
                m_PlayerData.FilledNodesX.Remove(m_PlayerData.FilledNodesX[_filledCount]);
                m_PlayerData.FilledNodesY.Remove(m_PlayerData.FilledNodesY[_filledCount]);
                m_PlayerData.FilledNodesLevel.Remove(m_PlayerData.FilledNodesLevel[_filledCount]);
            }
        }
        GameManager.Instance.JsonConverter.SavePlayerData(m_PlayerData);
    }
    public void SetFilledNodes()
    {
        PlayerFilledNodes.Clear();
        for (int _filledCount = 0; _filledCount < m_PlayerData.FilledNodesLevel.Count; _filledCount++)
        {
            PlayerFilledNodes.Add(GameManager.Instance.Entities.MergingPlatform.MergingGrid.GetNode(m_PlayerData.FilledNodesX[_filledCount], m_PlayerData.FilledNodesY[_filledCount]), m_PlayerData.FilledNodesLevel[_filledCount]);
        }
    }
    private void Update()
    {
        PlayerStateMachine.LogicalUpdate();
    }
}
