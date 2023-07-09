using UnityEngine;
using System.Collections.Generic;
using System;
using DG.Tweening;

public class Player : CustomBehaviour<PlayerManager>
{
    [SerializeField] private PlayerMovementData m_PlayerMovementData;
    [SerializeField] private Rigidbody m_PlayerRB;
    [SerializeField] private Transform m_GunParent;
    private PlayerData m_PlayerData;
    private List<Gun> m_Guns;

    public PlayerStateMachine PlayerStateMachine { get; private set; }
    #region Event
    public event Action OnChangedCoinValue;
    public event Action OnCompleteMergingState;
    public event Action<int> OnShooting;
    #endregion

    #region ShootData
    [HideInInspector] public float ShootRate;
    [HideInInspector] public int ShootCount;
    [HideInInspector] public float BulletLifeTime;
    #endregion

    #region ExternalAccess
    public Dictionary<Node, int> PlayerFilledNodes;
    public int PlayerLevel => m_PlayerData.PlayerLevel;
    public int PlayerCoin => m_PlayerData.PlayerCoin;
    public int GunCount => m_Guns.Count;
    public Transform GunParent => m_GunParent;
    #endregion
    public override void Initialize(PlayerManager _playerManager)
    {
        GameManager.Instance.JsonConverter.LoadPlayerData(ref m_PlayerData);
        PlayerStateMachine = new PlayerStateMachine(this);
        PlayerFilledNodes = new Dictionary<Node, int>();
        m_Guns = new List<Gun>();
        m_ChangeStateDelayID = GetInstanceID() + "m_ChangeStateDelayID";
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
    public void SetGunList(ListOperations _operation, Gun _gun)
    {
        switch (_operation)
        {
            case ListOperations.Adding:
                if (!m_Guns.Contains(_gun))
                {
                    m_Guns.Add(_gun);
                }
                break;
            case ListOperations.Removing:
                if (m_Guns.Contains(_gun))
                {
                    m_Guns.Remove(_gun);
                }
                break;
        }
    }
    public Gun GetGun(int _index)
    {
        return m_Guns[_index];
    }
    public void CompleteMergingState()
    {
        OnCompleteMergingState?.Invoke();
        GameStartDelayCall();
    }
    private string m_ChangeStateDelayID;
    private void GameStartDelayCall()
    {
        DOTween.Kill(m_ChangeStateDelayID);
        DOVirtual.DelayedCall(0.5f, () => GameManager.Instance.GameStart()).SetId(m_ChangeStateDelayID);
    }
    public void Shoot()
    {
        OnShooting?.Invoke(ShootCount);
    }
    public void PlayerForwardMovement()
    {
        m_PlayerRB.velocity = m_PlayerMovementData.PlayerForwardSpeed * Vector3.forward;
    }
    private Vector3 m_TempTargetGunsPos;
    public void GunHorizontalMovement()
    {
        m_TempTargetGunsPos = Vector3.Lerp(m_GunParent.position, m_GunParent.position + Vector3.right * m_HorizontalChangeValue * m_PlayerMovementData.PlayerHorizontalSpeed, Time.deltaTime * 8.0f);
        m_TempTargetGunsPos.x = Mathf.Clamp(m_TempTargetGunsPos.x, -2.5f, 2.5f);
        m_GunParent.position = m_TempTargetGunsPos;
    }
    private float m_HorizontalChangeValue;
    public void SetHorizontalChangeValue(Vector2 _swipe)
    {
        m_HorizontalChangeValue = _swipe.x;
        GunHorizontalMovement();
    }
    private void Update()
    {
        PlayerStateMachine.LogicalUpdate();
    }
    private void FixedUpdate()
    {
        PlayerStateMachine.PhysicalUpdate();
    }
}
