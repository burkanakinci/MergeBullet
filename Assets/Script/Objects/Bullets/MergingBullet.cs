using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MergingBullet : Bullet
{
    [SerializeField] private MergingBulletData m_MergingBulletData;
    private Node m_CurrentNode;
    public override void Initialize()
    {
        base.Initialize();
        m_ClickedSequenceID = GetInstanceID() + "m_ClickedSequenceID";
        m_ClickedSequence = DOTween.Sequence().SetId(m_ClickedSequenceID);
        m_MergingBulletMoveTweenID = GetInstanceID() + "m_MergingBulletMoveTweenID";
    }
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        SetIsKinematic(true);
    }
    public override void OnObjectDeactive()
    {
        base.OnObjectDeactive();
    }
    public void SetCurrentNode(Node _node)
    {
        m_CurrentNode = _node;
    }
    private void SetIsKinematic(bool _isKinematic)
    {
        m_BulletRB.isKinematic=_isKinematic;
    }
    private string m_ClickedSequenceID;
    private Sequence m_ClickedSequence;
    private Vector3 m_ClickedTargetPos;
    public void CLickedMergingBullet()
    {
        m_ClickedTargetPos = m_CurrentNode.transform.position + Vector3.up * m_MergingBulletData.ClickedHeight;
        m_ClickedSequence.Kill();
        m_ClickedSequence.Append(m_BulletVisual.BulletVisualScaleTween(Vector3.one * m_MergingBulletData.ClickedScaleMultiplier, m_MergingBulletData.ClickedScaleDuration).SetEase(m_MergingBulletData.ClickedScaleEase));
        m_ClickedSequence.Append(MergingBulletMoveTween(m_ClickedTargetPos, m_MergingBulletData.ClickedMovementDuration).SetEase(m_MergingBulletData.ClickedMovementEase));
    }
    public void ClickUpMergingBullet()
    {
        m_ClickedSequence.Kill();
        m_ClickedSequence.Append(m_BulletVisual.BulletVisualScaleTween(Vector3.one, m_MergingBulletData.ClickedUpMovementDuration).SetEase(m_MergingBulletData.ClickedUpMovementEase));
        m_ClickedSequence.Append(MergingBulletMoveTween(m_CurrentNode.transform.position, m_MergingBulletData.ClickedUpMovementDuration).SetEase(m_MergingBulletData.ClickedUpMovementEase));
    }
    private string m_MergingBulletMoveTweenID;
    public Tween MergingBulletMoveTween(Vector3 _target, float _duration)
    {
        DOTween.Kill(m_MergingBulletMoveTweenID);
        return transform.DOMove(_target, _duration);
    }
    private void KillAllTween()
    {
        m_ClickedSequence.Kill();
        DOTween.Kill(m_MergingBulletMoveTweenID);
    }
}
