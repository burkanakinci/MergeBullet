using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotButton : UIBaseButton<MergingPanel>
{   
    [SerializeField]private Transform m_ButtonVisual;
    public override void Initialize(MergingPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);
    }
    protected override void OnClickAction()
    {
        Debug.Log("Shoooot!!!");
        GameManager.Instance.Entities.MergingPlatform.ShootMergingBullets();
    }
}
