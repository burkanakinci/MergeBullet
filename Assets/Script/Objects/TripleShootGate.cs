using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TripleShootGate : PooledObject
{
    [SerializeField]private TripleShootGateData m_TripleShootGateData;
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void OnObjectSpawn()
    {
        gameObject.layer = (int)ObjectsLayer.Gate;
        transform.localScale=Vector3.one;
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        KillAllTween();
        base.OnObjectDeactive();
    }
    private string m_ScaleTweenID;
    private Tween GateScaleTween(Vector3 _target, float _duration)
    {
        DOTween.Kill(m_ScaleTweenID);
        return transform.DOScale(_target, _duration).SetId(m_ScaleTweenID);
    }
    public void DestroyTripleShootGate()
    {
        GateScaleTween(Vector3.zero, m_TripleShootGateData.DestroyTweenDuration).SetEase(m_TripleShootGateData.DestroyTweenEase)
        .OnComplete(OnObjectDeactive);
    }
    private void KillAllTween()
    {
        DOTween.Kill(m_ScaleTweenID);
    }
}
