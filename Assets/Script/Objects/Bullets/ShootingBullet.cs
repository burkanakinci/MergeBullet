using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : Bullet
{
    private float m_LifeCounter;
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        m_LifeCounter = 0.0f;
    }
    public override void OnObjectDeactive()
    {
        base.OnObjectDeactive();
    }
    private void Update()
    {
        if (m_LifeCounter < 2.0f)
            m_LifeCounter += Time.deltaTime;
        else
        {
            OnObjectDeactive();
        }
    }
    private void FixedUpdate()
    {
        SetVelocity();
    }
}
