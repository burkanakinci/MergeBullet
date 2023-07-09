using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncreaseGate : PooledObject
{
    [SerializeField] Color m_PositiveGateColor;
    [SerializeField] Color m_NegativeGateColor;
    [SerializeField] Color m_ZeroGateColor;
    [SerializeField] private SpriteRenderer m_GateSprite;
    [SerializeField] private TextMeshPro m_GateText;
    public int GateValue ;
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void OnObjectSpawn()
    {
        gameObject.layer = (int)ObjectsLayer.Gate;
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        base.OnObjectDeactive();
    }
    private void SetGateValue(int _value)
    {
        GateValue = _value;
        SetSpriteColor();
    }
    private void SetSpriteColor()
    {
        if (GateValue > 0)
        {
            m_GateSprite.color = m_PositiveGateColor;
        }
        else if (GateValue < 0)
        {
            m_GateSprite.color = m_NegativeGateColor;
        }
        else
        {
            m_GateSprite.color = m_ZeroGateColor;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.SHOOTING_BULLET))
        {
            SetGateValue(GateValue + 1);
        }
    }
}