using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CameraData", menuName = "Camera Data")]
public class CameraData : ScriptableObject
{
    #region Datas
    [Header("On Merging Data")]
    [SerializeField] private Vector3 m_MergingPos;
    [SerializeField] private Quaternion m_MergingRot;
    #endregion
    #region ExternalAccess
    public Vector3 MergingPos => m_MergingPos;
    public Quaternion MergingRot => m_MergingRot;
    #endregion
}
