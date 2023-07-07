using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PooledObject
{
    [SerializeField] private int m_BulletLevel;
    [SerializeField] private Rigidbody m_BulletRB;
    [SerializeField] private BulletMovementData m_BulletMovementData;
    public override void Initialize()
    {
        base.Initialize();
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
