using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShootGate : PooledObject
{
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
}
