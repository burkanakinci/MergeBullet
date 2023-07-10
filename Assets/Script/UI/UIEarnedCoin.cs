using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIEarnedCoin : PooledObject
{
    [SerializeField] private Transform m_CoinVisual;
    [SerializeField] private float m_MoveDuration;
    [SerializeField] private Ease m_MoveEase;
    public override void Initialize()
    {
        base.Initialize();
        m_CoinMovementTweenID = GetInstanceID() + "m_CoinMovementTweenID";
    }
    public override void OnObjectSpawn()
    {
        CoinMovementTween();
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        KillAllTween();
        base.OnObjectDeactive();
    }
    private string m_CoinMovementTweenID;
    private void CoinMovementTween()
    {
        DOTween.Kill(m_CoinMovementTweenID);
        transform.DOLocalMove(Vector3.zero, m_MoveDuration).SetEase(m_MoveEase).SetId(m_CoinMovementTweenID)
        .OnComplete(() =>
        {
            GameManager.Instance.PlayerManager.Player.SetPlayerCoin(GameManager.Instance.PlayerManager.Player.PlayerCoin + 100);
            OnObjectDeactive();
        });
    }
    private void KillAllTween()
    {
        DOTween.Kill(m_CoinMovementTweenID);
    }
}
