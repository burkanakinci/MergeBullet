using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class MergingBullet : Bullet
{
    [SerializeField] private TextMeshPro m_BulletText;
    [SerializeField] private MergingBulletData m_MergingBulletData;
    private Node m_CurrentNode;
    public override void Initialize()
    {
        base.Initialize();
        m_ClickedSequenceID = GetInstanceID() + "m_ClickedSequenceID";
        m_MergingBulletMoveTweenID = GetInstanceID() + "m_MergingBulletMoveTweenID";
        m_BulletText.text = "LV." + BulletLevel.ToString();
        m_MergedSpawnTag = PooledObjectTags.CONST_MERGING_BULLET + (BulletLevel + 1).ToString();
    }
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        SetIsKinematic(true);
        GameManager.Instance.Entities.MergingPlatform.OnShootMergingBullets += OnShoot;
    }
    public override void OnObjectDeactive()
    {
        KillAllTween();
        m_CurrentNode.SetBulletOnNode(null);
        GameManager.Instance.Entities.MergingPlatform.OnShootMergingBullets -= OnShoot;
        GameManager.Instance.Entities.MergingPlatform.SetMergingBulletList(ListOperations.Substraction, this);
        base.OnObjectDeactive();
    }
    public void SetCurrentNode(Node _node)
    {
        m_CurrentNode = _node;
    }
    private void SetIsKinematic(bool _isKinematic)
    {
        m_BulletRB.isKinematic = _isKinematic;
    }
    private string m_ClickedSequenceID;
    private Sequence m_ClickedSequence;
    private Vector3 m_ClickedTargetPos;
    public void CLickedMergingBullet()
    {
        m_ClickedSequence.Kill();
        m_ClickedSequence = DOTween.Sequence().SetId(m_ClickedSequenceID);
        m_ClickedTargetPos = m_CurrentNode.transform.position + Vector3.up * m_MergingBulletData.ClickedHeight;
        m_ClickedSequence.Append(m_BulletVisual.BulletVisualScaleTween(Vector3.one * m_MergingBulletData.ClickedScaleMultiplier, m_MergingBulletData.ClickedScaleDuration).SetEase(m_MergingBulletData.ClickedScaleEase));
        m_ClickedSequence.Join(MergingBulletMoveTween(m_ClickedTargetPos, m_MergingBulletData.ClickedMovementDuration).SetEase(m_MergingBulletData.ClickedMovementEase));
    }
    public void ClickedMovementMergingBullet(Vector3 _pos)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(_pos.x, transform.position.y, _pos.z), Time.deltaTime * 10.0f);
    }
    private MergingBullet m_TempSpawnedBullet;
    private string m_MergedSpawnTag;
    public void CheckMerginBullet(Node _clickedUpNode)
    {
        if (_clickedUpNode.BulletOnNode != null && _clickedUpNode.BulletOnNode != this && BulletLevel < 4)
        {
            if (_clickedUpNode.BulletOnNode.BulletLevel == BulletLevel)
            {
                ClickUpMergingBullet(_clickedUpNode.BulletOnNode.transform.position,
                () =>
                {
                    m_TempSpawnedBullet = GameManager.Instance.ObjectPool.SpawnFromPool(
                        m_MergedSpawnTag,
                        _clickedUpNode.transform.position,
                        Quaternion.identity,
                        GameManager.Instance.Entities.GetActiveParent(ActiveParents.MergingBulletParent)
                    ).GetGameObject().GetComponent<MergingBullet>();
                    _clickedUpNode.BulletOnNode.OnObjectDeactive();
                    GameManager.Instance.PlayerManager.Player.RemovePlayerFilledNodes(_clickedUpNode);
                    GameManager.Instance.PlayerManager.Player.RemovePlayerFilledNodes(m_CurrentNode);
                    _clickedUpNode.SetBulletOnNode(m_TempSpawnedBullet);
                    GameManager.Instance.PlayerManager.Player.AddPlayerFilledNodes(_clickedUpNode, m_TempSpawnedBullet.BulletLevel);
                    OnObjectDeactive();
                });
            }
            else
            {
                ClickUpMergingBullet(m_CurrentNode.transform.position, () => { });
            }
        }
        else
        {
            m_CurrentNode.SetBulletOnNode(null);
            GameManager.Instance.PlayerManager.Player.RemovePlayerFilledNodes(m_CurrentNode);
            _clickedUpNode.SetBulletOnNode(this);
            GameManager.Instance.PlayerManager.Player.AddPlayerFilledNodes(_clickedUpNode, BulletLevel);
            ClickUpMergingBullet(m_CurrentNode.transform.position, () => { });
        }
    }
    public void ClickUpMergingBullet(Vector3 _targetPos, Action _onComplete)
    {
        m_ClickedSequence.Kill();
        m_ClickedSequence = DOTween.Sequence().SetId(m_ClickedSequenceID);
        m_ClickedSequence.Append(m_BulletVisual.BulletVisualScaleTween(Vector3.one, m_MergingBulletData.ClickedUpMovementDuration).SetEase(m_MergingBulletData.ClickedUpMovementEase));
        m_ClickedSequence.Join(MergingBulletMoveTween(_targetPos, m_MergingBulletData.ClickedUpMovementDuration).SetEase(m_MergingBulletData.ClickedUpMovementEase));
        m_ClickedSequence.AppendCallback(() => _onComplete?.Invoke());
    }
    private string m_MergingBulletMoveTweenID;
    public Tween MergingBulletMoveTween(Vector3 _target, float _duration)
    {
        DOTween.Kill(m_MergingBulletMoveTweenID);
        return transform.DOMove(_target, _duration);
    }
    private void OnShoot()
    {
        SetIsKinematic(false);
        GameManager.Instance.Entities.MergingPlatform.SetMergingBulletList(ListOperations.Adding, this);
    }
    private void KillAllTween()
    {
        m_ClickedSequence.Kill();
        DOTween.Kill(m_MergingBulletMoveTweenID);
    }
    private void OnDestroy()
    {
        KillAllTween();
        GameManager.Instance.Entities.MergingPlatform.OnShootMergingBullets -= OnShoot;
    }
}
