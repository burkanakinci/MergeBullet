using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletVisual : CustomBehaviour<Bullet>
{
    public override void Initialize(Bullet _cachedComponent)
    {
        base.Initialize(_cachedComponent);
        m_BulletVisualScaleTweenID = GetInstanceID() + "m_BulletVisualScaleTweenID";
    }
    private string m_BulletVisualScaleTweenID;
    public Tween BulletVisualScaleTween(Vector3 _target, float _duration)
    {
        DOTween.Kill(m_BulletVisualScaleTweenID);
        return transform.DOScale(_target, _duration);
    }
}
