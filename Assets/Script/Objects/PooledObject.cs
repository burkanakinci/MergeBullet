using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : CustomBehaviour, IPooledObject
{
    private DeactiveParents m_DeactiveParent;
    public string PooledObjectTag { get; private set; }
    public override void Initialize()
    {
    }
    public virtual void OnObjectSpawn()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject += OnObjectDeactive;
    }
    public virtual void OnObjectDeactive()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;
        GameManager.Instance.ObjectPool.AddObjectPool(PooledObjectTag, this);
        this.transform.SetParent(GameManager.Instance.Entities.GetDeactiveParent(m_DeactiveParent));
        this.gameObject.SetActive(false);
    }
    public CustomBehaviour GetGameObject()
    {
        return this;
    }
    public void SetDeactiveParent(DeactiveParents _deactiveParent)
    {
        m_DeactiveParent = _deactiveParent;
    }
    public void SetPooledObjectTag(string _tag)
    {
        PooledObjectTag = _tag;
    }
}
