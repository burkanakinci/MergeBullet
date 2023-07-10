using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudPanel : UIPanel
{
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
    }
    public override void ShowPanel()
    {
        m_PanelAreas.ForEach(_area =>
        {
            _area.ShowArea();
        });
        base.ShowPanel();
    }
}
