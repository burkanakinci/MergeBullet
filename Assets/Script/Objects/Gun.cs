using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : PooledObject
{
    [HideInInspector] public int SpawnBulletLevel;
    [SerializeField] private GunVisual m_GunVisual;
    [SerializeField] private Transform m_BulletSpawnPoint;
    [SerializeField] private Transform m_MergingBulletJumpPoint;
    [SerializeField] private GunData m_GunData;
    public Vector3 BulletJumpPoint => m_MergingBulletJumpPoint.position;
    public override void Initialize()
    {
        base.Initialize();
        m_GunMoveTweenID = GetInstanceID() + "m_GunMoveTweenID";
    }
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        SpawnBulletLevel = -1;
        GameManager.Instance.PlayerManager.Player.SetGunList(ListOperations.Adding, this);
        GameManager.Instance.PlayerManager.Player.OnCompleteMergingState += OnCompleteMergingState;
        GameManager.Instance.PlayerManager.Player.OnShooting += GunShoot;
    }
    public override void OnObjectDeactive()
    {
        GameManager.Instance.PlayerManager.Player.SetGunList(ListOperations.Removing, this);
        m_GunVisual.KillAllTween();
        GameManager.Instance.PlayerManager.Player.OnCompleteMergingState -= OnCompleteMergingState;
        GameManager.Instance.PlayerManager.Player.OnShooting -= GunShoot;
        base.OnObjectDeactive();
    }
    private string m_GunMoveTweenID;
    private Tween GunMoveTween(Vector3 _target, float _duration, bool _isLocal)
    {
        DOTween.Kill(m_GunMoveTweenID);
        if (!_isLocal)
            return transform.DOMove(_target, _duration);
        else
            return transform.DOLocalMove(_target, _duration);
    }
    private Vector3 m_LocalGunPos;
    private Vector2 m_RandomPoint;
    private void OnCompleteMergingState()
    {
        if (SpawnBulletLevel < 1)
        {
            m_GunVisual.GunVisualScaleTween(Vector3.zero, m_GunData.DestroyTweenDuration).SetEase(m_GunData.DestroyTweenEase)
            .OnComplete(OnObjectDeactive);
        }
        else
        {
            m_RandomPoint = Random.insideUnitCircle * (GameManager.Instance.PlayerManager.Player.GunCount * 0.2f);
            m_LocalGunPos = new Vector3(m_RandomPoint.x, m_RandomPoint.y);
            GunMoveTween(m_LocalGunPos, m_GunData.LocalMoveDuration, true);
        }
    }
    private float m_TempBulletPointEuler;
    private void GunShoot(int _shootCount)
    {
        for (int _shoot = 0; _shoot < _shootCount; _shoot++)
        {
            m_TempBulletPointEuler = (_shoot % 2 == 1) ? (-20.0f) : (10.0f * _shoot);
            GameManager.Instance.ObjectPool.SpawnFromPool(
                PooledObjectTags.CONST_SHOOTING_BULLET + SpawnBulletLevel,
                m_BulletSpawnPoint.position,
                Quaternion.Euler(Vector3.up * m_TempBulletPointEuler),
                GameManager.Instance.Entities.GetActiveParent(ActiveParents.ShootingBulletActiveParent)
            );
        }
    }
    private void KillAllTween()
    {
        DOTween.Kill(m_GunMoveTweenID);
        m_GunVisual.KillAllTween();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        KillAllTween();
        GameManager.Instance.PlayerManager.Player.OnCompleteMergingState -= OnCompleteMergingState;
        GameManager.Instance.PlayerManager.Player.OnShooting -= GunShoot;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.PlayerManager.Player.PlayerStateMachine.GetPlayerState(PlayerStates.RunState).TriggerEnter(other);
    }
}
