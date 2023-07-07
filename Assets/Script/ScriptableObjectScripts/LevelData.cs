using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    #region Datas
    public int LevelNumber;
    public int RoadCount;
    public int MergingGridWeight;
    public int MergingGridHeight;
    #endregion
}
