using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : PooledObject
{
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void OnObjectSpawn()
    {
        gameObject.layer = (int)ObjectsLayer.Obstacle;
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        base.OnObjectDeactive();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.GUN) && gameObject.layer == (int)ObjectsLayer.Obstacle)
        {
            gameObject.layer = (int)ObjectsLayer.Default;
            GameManager.Instance.LevelFailed();
        }
    }
}
