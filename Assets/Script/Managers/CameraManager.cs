using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class CameraManager : CustomBehaviour
{
    [SerializeField] private Camera m_MainCamera;
    [SerializeField] private CameraData m_CameraData;
    private bool m_FollowMergingBullet;
    public override void Initialize()
    {
        m_CameraTweenID = GetInstanceID() + "m_RunStateTweenID";
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.MergingState).OnEnterEvent += OnMergingState;
        GameManager.Instance.Entities.MergingPlatform.OnShootMergingBullets += OnShoot;
    }
    public void FollowMergingBullet()
    {
        if (m_FollowMergingBullet && GameManager.Instance.Entities.MergingPlatform.GetAverageMergingBullets() > m_CameraData.MergingPos.z)
        {
            m_MainCamera.transform.position = new Vector3(m_MainCamera.transform.position.x, m_MainCamera.transform.position.y, GameManager.Instance.Entities.MergingPlatform.GetAverageMergingBullets());
        }
    }
    public void FolowPlayer()
    {
        m_MainCamera.transform.position = GameManager.Instance.PlayerManager.Player.GunParent.position + m_CameraData.PlayerFollowOffset;
    }
    private string m_CameraTweenID;
    private Vector3 m_RunStartPos;
    private Vector3 m_RunEndPos;
    private Quaternion m_RunStartRot;
    private void RunStateCameraTween()
    {
        m_RunStartPos = m_MainCamera.transform.position;
        m_RunEndPos = GameManager.Instance.PlayerManager.Player.transform.position + m_CameraData.PlayerFollowOffset;
        m_RunStartRot = m_MainCamera.transform.rotation;
        DOTween.Kill(m_CameraTweenID);
        DOTween.To(() => 0.0f, x => UpdateRunStateTween(x), 1.0f, m_CameraData.RunTweenDuration).SetEase(m_CameraData.RunTweenEase);
    }
    private void UpdateRunStateTween(float _lerpValue)
    {
        m_MainCamera.transform.position = Vector3.Lerp(m_RunStartPos, m_RunEndPos, _lerpValue);
        m_MainCamera.transform.rotation = Quaternion.Lerp(m_RunStartRot, m_CameraData.ShootRot, _lerpValue);
    }
    #region Event
    private void OnMergingState()
    {
        DOTween.Kill(m_CameraTweenID);
        m_FollowMergingBullet = false;
        m_MainCamera.transform.position = m_CameraData.MergingPos;
        m_MainCamera.transform.rotation = m_CameraData.MergingRot;
    }
    private void OnShoot()
    {
        m_FollowMergingBullet = true;
    }
    public void OnCompleteMergingState()
    {
        if (!m_FollowMergingBullet)
            return;
        m_FollowMergingBullet = false;
        RunStateCameraTween();
    }
    private void OnDestroy()
    {
        DOTween.Kill(m_CameraTweenID);
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.MergingState).OnEnterEvent -= OnMergingState;
        GameManager.Instance.Entities.MergingPlatform.OnShootMergingBullets -= OnShoot;
    }
    #endregion
}
