using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunVisual : CustomBehaviour<Gun>
{
    public override void Initialize(Gun _cachedComponent)
    {
        base.Initialize(_cachedComponent);
        m_GunVisualScaleTweenID = GetInstanceID() + "m_GunVisualScaleTweenID";
    }
    private string m_GunVisualScaleTweenID;
    public Tween GunVisualScaleTween(Vector3 _target, float _duration)
    {
        DOTween.Kill(m_GunVisualScaleTweenID);
        return transform.DOScale(_target, _duration).SetId(m_GunVisualScaleTweenID);
    }
    public void KillAllTween()
    {
        DOTween.Kill(m_GunVisualScaleTweenID);
    }
}
