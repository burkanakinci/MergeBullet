using System;
using UnityEngine;
public class LevelManager : CustomBehaviour
{
    #region Fields
    private LevelData m_CurrentLevelData;
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
        m_CurrentLevelData = Resources.Load<LevelData>("LevelDatas/" + m_ActiveLevelDataNumber + "LevelData");
    }
    private void SpawnSceneObjects()
    {
    }
    #region SpawnSceneObjects

    #endregion
}