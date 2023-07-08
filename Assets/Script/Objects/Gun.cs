using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : PooledObject
{
    [SerializeField] private GunVisual m_GunVisual;
    [SerializeField] private Transform m_BulletSpawnPoint;
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
