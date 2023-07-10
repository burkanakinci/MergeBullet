using System;
using UnityEngine;
using System.Collections;
public class LevelManager : CustomBehaviour
{
    #region Fields
    public LevelData CurrentLevelData { get; private set; }
    private int m_CurrentLevelNumber;
    private int m_ActiveLevelDataNumber;
    private int m_MaxLevelDataCount;
    #endregion

    #region Actions
    public event Action OnCleanSceneObject;
    #endregion
    public override void Initialize()
    {
        m_MaxLevelDataCount = Resources.LoadAll("LevelDatas", typeof(LevelData)).Length;
    }
    public void SetLevelNumber(int _levelNumber)
    {
        m_CurrentLevelNumber = _levelNumber;
        m_ActiveLevelDataNumber = (m_CurrentLevelNumber <= m_MaxLevelDataCount) ? (m_CurrentLevelNumber) : ((int)(UnityEngine.Random.Range(1, (m_MaxLevelDataCount + 1))));
    }
    public void CreateLevel()
    {
        CleanSceneObject();
        GetLevelData();
        SpawnSceneObjects();
    }
    public void CleanSceneObject()
    {
        OnCleanSceneObject?.Invoke();
    }
    public void GetLevelData()
    {
        CurrentLevelData = Resources.Load<LevelData>("LevelDatas/" + m_ActiveLevelDataNumber + "LevelData");
    }
    private void SpawnSceneObjects()
    {
        SpawnRoad();
        SpawnGate();
        SpawnObstacle();
        SpawnCollectable();
    }
    #region SpawnSceneObjects
    #region SpawnRoad
    private void SpawnRoad()
    {
        for (int _roadCount = 0; _roadCount < CurrentLevelData.RoadCount; _roadCount++)
        {
            GameManager.Instance.ObjectPool.SpawnFromPool(
                PooledObjectTags.ROAD,
                Vector3.forward * _roadCount * 10.0f + Vector3.forward * 10.0f,
                Quaternion.identity,
                GameManager.Instance.Entities.GetActiveParent(ActiveParents.RoadActiveParent)
            );
        }
    }
    #endregion
    #region SpawnGate
    private void SpawnGate()
    {
        SpawnTripleShootGate();
        SpawnFireGate();
        SpawnRangeGate();
    }
    private void SpawnTripleShootGate()
    {
        for (int _tripleCount = 0; _tripleCount < CurrentLevelData.TripleShootPositions.Count; _tripleCount++)
        {
            GameManager.Instance.ObjectPool.SpawnFromPool(
                PooledObjectTags.TRIPLE_SHOOT_GATE,
                CurrentLevelData.TripleShootPositions[_tripleCount],
                Quaternion.identity,
                GameManager.Instance.Entities.GetActiveParent(ActiveParents.GateActiveParent)
            );
        }
    }
    private IncreaseGate m_TempSpawnedGate;
    private void SpawnFireGate()
    {
        for (int _fireCount = 0; _fireCount < CurrentLevelData.FireGatePositions.Count; _fireCount++)
        {
            m_TempSpawnedGate = GameManager.Instance.ObjectPool.SpawnFromPool(
                PooledObjectTags.FIRE_RATE_GATE,
                CurrentLevelData.FireGatePositions[_fireCount],
                Quaternion.identity,
                GameManager.Instance.Entities.GetActiveParent(ActiveParents.GateActiveParent)
            ).GetGameObject().GetComponent<IncreaseGate>();
            m_TempSpawnedGate.SetGateValue(CurrentLevelData.FireGateSpawnValues[_fireCount]);
        }
    }
    private void SpawnRangeGate()
    {
        for (int _rangeCount = 0; _rangeCount < CurrentLevelData.RangeGatePositions.Count; _rangeCount++)
        {
            m_TempSpawnedGate = GameManager.Instance.ObjectPool.SpawnFromPool(
                PooledObjectTags.RANGE_GATE,
                CurrentLevelData.RangeGatePositions[_rangeCount],
                Quaternion.identity,
                GameManager.Instance.Entities.GetActiveParent(ActiveParents.GateActiveParent)
            ).GetGameObject().GetComponent<IncreaseGate>();
            m_TempSpawnedGate.SetGateValue(CurrentLevelData.RangeGateSpawnValues[_rangeCount]);
        }
    }
    #endregion
    #region SpawnObstacle
    private void SpawnObstacle()
    {
        SpawnObstacleWall();
    }
    private void SpawnObstacleWall()
    {
        for (int _wallCount = 0; _wallCount < CurrentLevelData.ObstacleWallPositions.Count; _wallCount++)
        {
            GameManager.Instance.ObjectPool.SpawnFromPool(
               PooledObjectTags.OBSTACLE_WALL,
               CurrentLevelData.ObstacleWallPositions[_wallCount],
               Quaternion.identity,
               GameManager.Instance.Entities.GetActiveParent(ActiveParents.ObstacleActiveParent));
        }
    }
    public void StopMissileSpawnCoroutine()
    {
        if (m_MissileSpawnCoroutine != null)
        {
            StopCoroutine(m_MissileSpawnCoroutine);
        }
    }
    private Coroutine m_MissileSpawnCoroutine;
    public void StartMissileSpawnCoroutine()
    {
        if(CurrentLevelData.UseMissile)
        {
        StopMissileSpawnCoroutine();
        StartCoroutine(MissileSpawnCoroutine());
        }
    }
    private IEnumerator MissileSpawnCoroutine()
    {
        yield return new WaitForSeconds(CurrentLevelData.MissileSpawnRate);
        GameManager.Instance.ObjectPool.SpawnFromPool(
            PooledObjectTags.OBSTACLE_MISSILE,
            GameManager.Instance.PlayerManager.Player.GunParent.position + Vector3.forward * 15.0f,
            Quaternion.identity,
            GameManager.Instance.Entities.GetActiveParent(ActiveParents.ObstacleActiveParent)
        );
        StartMissileSpawnCoroutine();
    }
    #endregion
    #region SpawnCollectable
    private void SpawnCollectable()
    {
        SpawnCollectableCoin();
    }
    private void SpawnCollectableCoin()
    {
        for (int _coinCount = 0; _coinCount < CurrentLevelData.CollectableCoinPositions.Count; _coinCount++)
        {
            GameManager.Instance.ObjectPool.SpawnFromPool(
               PooledObjectTags.COLLECTABLE_COIN,
               CurrentLevelData.CollectableCoinPositions[_coinCount],
               Quaternion.identity,
               GameManager.Instance.Entities.GetActiveParent(ActiveParents.CollectableActiveParent));
        }
    }
    private void CollectableCollectableShield()
    {
        for (int _shield = 0; _shield < CurrentLevelData.CollectableShieldPositions.Count; _shield++)
        {
            GameManager.Instance.ObjectPool.SpawnFromPool(
               PooledObjectTags.COLLECTABLE_SHIELD,
               CurrentLevelData.CollectableShieldPositions[_shield],
               Quaternion.identity,
               GameManager.Instance.Entities.GetActiveParent(ActiveParents.CollectableActiveParent));
        }
    }
    #endregion
    #endregion
}