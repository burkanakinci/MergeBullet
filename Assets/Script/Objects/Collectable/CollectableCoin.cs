using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : PooledObject
{
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void OnObjectSpawn()
    {
        gameObject.layer = (int)ObjectsLayer.Collectable;
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        base.OnObjectDeactive();
    }
    private Vector3 m_ScreenPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.GUN)&&gameObject.layer!=(int)ObjectsLayer.Default)
        {
            gameObject.layer = (int)ObjectsLayer.Default;
            m_ScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            GameManager.Instance.ObjectPool.SpawnFromPool(
                PooledObjectTags.UI_COIN,
                m_ScreenPos,
                Quaternion.identity,
                GameManager.Instance.Entities.GetActiveParent(ActiveParents.CoinArea)
            );
            OnObjectDeactive();
        }
    }
}
