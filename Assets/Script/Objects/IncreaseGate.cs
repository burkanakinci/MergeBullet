using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class IncreaseGate : PooledObject
{
    [SerializeField] private IncreaseGateData m_IncreaseGateData;
    [SerializeField] private SpriteRenderer m_GateSprite;
    [SerializeField] private TextMeshPro m_GateText;
    [SerializeField] private int m_GateValue;
    public int GateValue => m_GateValue;
    public override void Initialize()
    {
        base.Initialize();
        m_ScaleTweenID = GetInstanceID() + "m_DestroyTweenID";
    }
    public override void OnObjectSpawn()
    {
        gameObject.layer = (int)ObjectsLayer.Gate;
        transform.localScale = Vector3.one;
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        KillAllTween();
        base.OnObjectDeactive();
    }
    public void SetGateValue(int _value)
    {
        m_GateValue = _value;
        m_GateText.text = GateValue.ToString();
        SetSpriteColor();
    }
    private void SetSpriteColor()
    {
        if (GateValue > 0)
        {
            m_GateSprite.color = m_IncreaseGateData.PositiveGateColor;
        }
        else if (GateValue < 0)
        {
            m_GateSprite.color = m_IncreaseGateData.NegativeGateColor;
        }
        else
        {
            m_GateSprite.color = m_IncreaseGateData.ZeroGateColor;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.SHOOTING_BULLET))
        {
            GateScaleTween(Vector3.one * 1.1f, m_IncreaseGateData.PunchScaleUpDuration).SetEase(m_IncreaseGateData.PunchScaleUpnEase)
            .OnComplete(() => GateScaleTween(Vector3.one, m_IncreaseGateData.PunchScaleDownDuration).SetEase(m_IncreaseGateData.PunchScaleDownnEase));
            SetGateValue(GateValue + 1);
        }
    }
    private string m_ScaleTweenID;
    private Tween GateScaleTween(Vector3 _target, float _duration)
    {
        DOTween.Kill(m_ScaleTweenID);
        return transform.DOScale(_target, _duration).SetId(m_ScaleTweenID);
    }
    public void DestroyIncreaseGate()
    {
        GateScaleTween(Vector3.zero, m_IncreaseGateData.DestroyTweenDuration).SetEase(m_IncreaseGateData.DestroyTweenEase)
        .OnComplete(OnObjectDeactive);
    }
    private void KillAllTween()
    {
        DOTween.Kill(m_ScaleTweenID);
    }
}