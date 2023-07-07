using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : PooledObject
{
    [SerializeField] private Transform m_NodeVisual;
    public int NodeXIndis { get; private set; }
    public int NodeYIndis { get; private set; }
    public MergingBullet BulletOnNode { get; private set; }
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
    public void SetNode(int _x, int _y)
    {
        NodeXIndis = _x;
        NodeYIndis = _y;
    }
    public void SetBulletOnNode(MergingBullet _bullet)
    {
        BulletOnNode = _bullet;
        BulletOnNode.SetCurrentNode(this);
    }
}
