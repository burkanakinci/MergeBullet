using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinArea : UIArea
{
    [SerializeField] private NextLevelButton m_NextLevelButton;
    public override void Initialize(UIPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);
        m_NextLevelButton.Initialize(CachedComponent);
    }
}
