using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    #region Datas
    public int RoadCount;
    #region Grid
    public int MergingGridWeight;
    public int MergingGridHeight;
    #endregion
    #region Gates
    public List<Vector3> TripleShootPositions;
    public List<Vector3> FireGatePositions;
    public List<int> FireGateSpawnValues;
    public List<Vector3> RangeGatePositions;
    public List<int> RangeGateSpawnValues;
    #endregion
    #region Obstacle
    public bool UseMissile;
    public float MissileSpawnRate;
    public List<Vector3> ObstacleWallPositions;
    #endregion
    #region Collectable
    public List<Vector3> CollectableShieldPositions;
    public List<Vector3> CollectableCoinPositions;
    #endregion
    #endregion
}
