using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MergingBullet : Bullet
{
    private Node m_CurrentNode;
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
    public void SetCurrentNode(Node _node)
    {
        m_CurrentNode = _node;
    }
}
