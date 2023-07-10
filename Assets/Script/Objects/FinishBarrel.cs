using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishBarrel : CustomBehaviour<Finish>
{
    [SerializeField] private int m_BarrelValue;
    [SerializeField] private TextMeshPro m_BarrelText;
    public override void Initialize(Finish _cachedComponent)
    {
        base.Initialize(_cachedComponent);
    }
    public void SetBarrelValue(int _value)
    {
        m_BarrelValue = _value;
        m_BarrelText.text = m_BarrelValue.ToString();
    }
    public void SetStartValue(int _startValue)
    {
        m_StartValue=_startValue;
    }
    private int m_StartValue;
    private Vector3 m_ScreenPos;
    private void SpawnBarrelCoin()
    {
        for (int _count = 0; _count < m_StartValue; _count++)
        {
            m_ScreenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.right * Random.Range(-0.75f, 0.75f));
            GameManager.Instance.ObjectPool.SpawnFromPool(
                PooledObjectTags.UI_COIN,
                m_ScreenPos,
                Quaternion.identity,
                GameManager.Instance.Entities.GetActiveParent(ActiveParents.CoinArea)
            );
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.SHOOTING_BULLET))
        {
            SetBarrelValue(m_BarrelValue - 1);
            if (m_BarrelValue <= 0)
            {
                SpawnBarrelCoin();
                gameObject.SetActive(false);
            }
        }
    }
}
