using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PooledObject
{
    [SerializeField] protected Rigidbody m_BulletRB;
    [SerializeField] protected BulletVisual m_BulletVisual;
    [SerializeField] protected BulletMovementData m_BulletMovementData;
    public override void Initialize()
    {
        base.Initialize();
        m_BulletVisual.Initialize(this);
    }
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        base.OnObjectDeactive();
    }
}
