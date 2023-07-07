using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MergingPlatform : CustomBehaviour
{
    public MergingGrid MergingGrid;
    public event Action OnShotBullet;
    public override void Initialize()
    {
        MergingGrid.Initialize(this);
    }
}
