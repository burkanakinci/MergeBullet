using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MergingGrid : CustomBehaviour<MergingPlatform>
{
    private int m_GridWeight;
    private int m_GridHeight;
    private Node[,] m_MergingNodes;
    public override void Initialize(MergingPlatform _cachedComponent)
    {
        base.Initialize(_cachedComponent);
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.MergingState).OnEnterEvent += CreateNodes;
    }
    #region CreateNode
    private Node m_TempSpawnedNode;
    private Vector3 m_TempNodePos;
    private Node m_TempFilledNode;
    private string m_TempSpawnedBulletTag;
    private void CreateNodes()
    {
        SetGridSize();
        SpawnNodes();
        GameManager.Instance.PlayerManager.Player.SetFilledNodes();
        SpawnFilledNodes();
        SpawnGuns();
    }
    private void SpawnNodes()
    {
        m_TempNodePos = Vector3.up * 0.1f;
        m_MergingNodes = new Node[m_GridWeight, m_GridHeight];
        for (int _y = 0; _y < m_GridHeight; _y++)
        {
            for (int _x = 0; _x < m_GridWeight; _x++)
            {
                SetSpawnedPos(_x, _y);
                m_TempSpawnedNode = GameManager.Instance.ObjectPool.SpawnFromPool(
                    PooledObjectTags.MERGING_NODE,
                    m_TempNodePos,
                    Quaternion.identity,
                    transform
                ).GetGameObject().GetComponent<Node>();
                m_MergingNodes[_x, _y] = m_TempSpawnedNode;
                m_TempSpawnedNode.SetNode(_x, _y);
            }
        }
    }
    private void SpawnFilledNodes()
    {
        for (int _filledCount = 0; _filledCount < GameManager.Instance.PlayerManager.Player.PlayerFilledNodes.Count; _filledCount++)
        {
            m_TempSpawnedBulletTag = PooledObjectTags.CONST_MERGING_BULLET + GameManager.Instance.PlayerManager.Player.PlayerFilledNodes.ElementAt(_filledCount).Value.ToString();
            m_TempFilledNode = GameManager.Instance.PlayerManager.Player.PlayerFilledNodes.ElementAt(_filledCount).Key;
            m_TempFilledNode.SetBulletOnNode(
                GameManager.Instance.ObjectPool.SpawnFromPool(
                    m_TempSpawnedBulletTag,
                    m_TempFilledNode.transform.position,
                    Quaternion.identity,
                    GameManager.Instance.Entities.GetActiveParent(ActiveParents.MergingBulletParent)
                    ).GetGameObject().GetComponent<MergingBullet>());
        }
    }
    private Vector3 m_TempSpawnGunPos;
    private void SpawnGuns()
    {
        for (int _x = 0; _x < m_MergingNodes.GetLength(0); _x++)
        {
            m_TempSpawnGunPos=m_MergingNodes[_x, 0].transform.position + Vector3.forward * 12.5f;
            m_TempSpawnGunPos.y=0.0f;
            GameManager.Instance.ObjectPool.SpawnFromPool(
                PooledObjectTags.GUN,
                m_TempSpawnGunPos,
                Quaternion.identity,
                GameManager.Instance.Entities.GetActiveParent(ActiveParents.GunActiveParent)
            );
        }
    }
    private void SetGridSize()
    {
        m_GridWeight = GameManager.Instance.LevelManager.CurrentLevelData.MergingGridWeight;
        m_GridHeight = GameManager.Instance.LevelManager.CurrentLevelData.MergingGridHeight;
    }
    private void SetSpawnedPos(int _x, int _y)
    {
        m_TempNodePos.x = m_GridWeight * -0.5f + 0.5f;
        m_TempNodePos.x += _x;
        m_TempNodePos.z = _y + 0.5f;
    }
    #endregion
    #region Getter
    public Node GetEmptyNode()
    {
        for (int _y = 0; _y < m_GridHeight; _y++)
        {
            for (int _x = 0; _x < m_GridWeight; _x++)
            {
                if (m_MergingNodes[_x, _y].BulletOnNode == null)
                {
                    return m_MergingNodes[_x, _y];
                }
            }
        }
        return null;
    }
    public Node GetNode(int _x, int _y)
    {
        return m_MergingNodes[_x, _y];
    }
    #endregion
    #region Event
    private void OnDestroy()
    {
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.MergingState).OnEnterEvent -= CreateNodes;
    }
    #endregion
}
