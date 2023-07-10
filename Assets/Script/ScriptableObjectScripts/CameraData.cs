using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "CameraData", menuName = "Camera Data")]
public class CameraData : ScriptableObject
{
    #region Datas
    [Header("On Merging Data")]
    [SerializeField] private Vector3 m_MergingPos;
    [SerializeField] private Quaternion m_MergingRot;

    [Space(10)]
    [Header("On Shoot")]
    [SerializeField] private Quaternion m_ShootRot;
    [SerializeField] private Vector3 m_PlayerFollowOffset;

    [Space(10)]
    [Header("Run State Tween Value")]
    [SerializeField] private float m_RunTweenDuration;
    [SerializeField] private Ease m_RunTweenEase = Ease.Linear;
    #endregion
    #region ExternalAccess
    public Vector3 MergingPos => m_MergingPos;
    public Quaternion MergingRot => m_MergingRot;

    public Quaternion ShootRot => m_ShootRot;
    public Vector3 PlayerFollowOffset => m_PlayerFollowOffset;

    public float RunTweenDuration => m_RunTweenDuration;
    public Ease RunTweenEase => m_RunTweenEase;
    #endregion
}
