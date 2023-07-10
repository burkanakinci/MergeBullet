using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            SetGateValue(GateValue + 1);
        }
    }
}