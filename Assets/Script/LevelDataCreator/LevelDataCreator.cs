#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

public class LevelDataCreator : MonoBehaviour
{
    [SerializeField] private Node m_NodePrefab;
    [HideInInspector] public LevelData TempLevelData;
    #region Level Datas
    [HideInInspector] public int LevelNumber;
    [HideInInspector] public int GridWeight;
    [HideInInspector] public int GridHeight;
    [HideInInspector] public bool UseMissile;
    [HideInInspector] public int MissileSpawnRate;
    #endregion

    #region  LevelDataFields
    private string m_SavePath;
    #endregion
    #region SceneFields
    #endregion
    public void CreateLevel()
    {

        AssetDatabase.DeleteAsset("Assets/Resources/LevelDatas/" + LevelNumber + "LevelData.asset");
        AssetDatabase.Refresh();

        TempLevelData = ScriptableObject.CreateInstance<LevelData>();
        m_SavePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/LevelDatas/" + LevelNumber + "LevelData.asset");

        #region Grid
        TempLevelData.MergingGridWeight = GridWeight;
        TempLevelData.MergingGridHeight = GridHeight;
        #endregion

        #region Gate
        TempLevelData.RoadCount = GameObject.FindGameObjectsWithTag(ObjectTags.ROAD).Length;
        TempLevelData.TripleShootPositions = new List<Vector3>();
        GameObject.FindGameObjectsWithTag(ObjectTags.TRIPLE_SHOOT_GATE).ToList().ForEach(_tripple =>
        {
            TempLevelData.TripleShootPositions.Add(_tripple.transform.position);
        });
        TempLevelData.FireGatePositions = new List<Vector3>();
        TempLevelData.FireGateSpawnValues = new List<int>();
        GameObject.FindGameObjectsWithTag(ObjectTags.FIRE_RATE_GATE).ToList().ForEach(_fire =>
        {
            TempLevelData.FireGatePositions.Add(_fire.transform.position);
            TempLevelData.FireGateSpawnValues.Add(_fire.GetComponent<IncreaseGate>().GateValue);
        });
        TempLevelData.RangeGatePositions = new List<Vector3>();
        TempLevelData.RangeGateSpawnValues = new List<int>();
        GameObject.FindGameObjectsWithTag(ObjectTags.RANGE_GATE).ToList().ForEach(_range =>
        {
            TempLevelData.RangeGatePositions.Add(_range.transform.position);
            TempLevelData.RangeGateSpawnValues.Add(_range.GetComponent<IncreaseGate>().GateValue);
        });
        #endregion
        #region Obstacle
        TempLevelData.ObstacleWallPositions = new List<Vector3>();
        GameObject.FindGameObjectsWithTag(ObjectTags.OBSTACLE).ToList().ForEach(_wall =>
        {
            TempLevelData.ObstacleWallPositions.Add(_wall.transform.position);
        });
        TempLevelData.UseMissile = UseMissile;
        TempLevelData.MissileSpawnRate = MissileSpawnRate;
        #endregion
        #region Collectable
        TempLevelData.CollectableCoinPositions = new List<Vector3>();
        GameObject.FindGameObjectsWithTag(ObjectTags.COLLECTABLE_COIN).ToList().ForEach(_coin =>
        {
            TempLevelData.CollectableCoinPositions.Add(_coin.transform.position);
        });
        TempLevelData.CollectableShieldPositions = new List<Vector3>();
        GameObject.FindGameObjectsWithTag(ObjectTags.COLLECTABLE_SHIELD).ToList().ForEach(_shield =>
        {
            TempLevelData.CollectableShieldPositions.Add(_shield.transform.position);
        });
        #endregion

        AssetDatabase.CreateAsset(TempLevelData, m_SavePath);
        AssetDatabase.SaveAssets();
    }
    private List<GameObject> m_Nodes = new List<GameObject>();
    public void SpawnNodesLevelData()
    {
        m_Nodes.ForEach(_node =>
        {
            DestroyImmediate(_node);
        });
        for (int _y = 0; _y < GridHeight; _y++)
        {
            for (int _x = 0; _x < GridWeight; _x++)
            {
                m_Nodes.Add(Instantiate(m_NodePrefab, GetSpawnedPos(_x, _y), Quaternion.identity).gameObject);
            }
        }
    }
    // private void SetSpawnedPos(int _x, int _y)
    // {
    //     m_TempNodePos.x = m_GridWeight * -0.5f + 0.5f;
    //     m_TempNodePos.x += _x;
    //     m_TempNodePos.z = _y + 0.5f;
    // }
    private Vector3 m_SpawnPos;
    private Vector3 GetSpawnedPos(int _x, int _y)
    {
        m_SpawnPos.y = 0.1f;
        m_SpawnPos.x = GridWeight * -0.5f + 0.5f;
        m_SpawnPos.x += _x;
        m_SpawnPos.z = _y + 0.5f;
        return m_SpawnPos;
    }
}
#endif